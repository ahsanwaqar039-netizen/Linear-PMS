"use client";

import React, { useState, useEffect } from "react";
import { 
  DndContext, 
  DragOverlay, 
  closestCorners, 
  KeyboardSensor, 
  PointerSensor, 
  useSensor, 
  useSensors, 
  DragStartEvent, 
  DragOverEvent, 
  DragEndEvent, 
  defaultDropAnimationSideEffects,
  DropAnimation
} from "@dnd-kit/core";
import { 
  arrayMove, 
  SortableContext, 
  sortableKeyboardCoordinates, 
  verticalListSortingStrategy,
  horizontalListSortingStrategy 
} from "@dnd-kit/sortable";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { issueService } from "@/lib/api";
import { KanbanColumn } from "@/components/KanbanColumn";
import { IssueCard } from "@/components/IssueCard";
import { motion } from "framer-motion";
import { useParams } from "next/navigation";

export type Status = "backlog" | "todo" | "in_progress" | "done" | "canceled";

export interface Issue {
  id: string;
  title: string;
  description?: string;
  priority: "low" | "medium" | "high" | "urgent";
  status: Status;
  assignee?: {
    name: string;
    avatar?: string;
  };
  key: string; 
}

const COLUMNS: { id: Status; label: string }[] = [
  { id: "backlog", label: "Backlog" },
  { id: "todo", label: "Todo" },
  { id: "in_progress", label: "In Progress" },
  { id: "done", label: "Done" },
];

export const KanbanBoard = () => {
  const { id: projectId } = useParams();
  const queryClient = useQueryClient();
  const [activeId, setActiveId] = useState<string | null>(null);

  // Fetch issues
  const { data: issues = [], isLoading } = useQuery({
    queryKey: ["issues", projectId],
    queryFn: async () => {
      const res = await issueService.getByProject(projectId as string);
      return res.data as Issue[];
    },
    enabled: !!projectId,
  });

  // Mutation for updating status
  const updateStatusMutation = useMutation({
    mutationFn: ({ id, status }: { id: string; status: string }) => 
      issueService.changeStatus(id, status),
    onMutate: async ({ id, status }) => {
      await queryClient.cancelQueries({ queryKey: ["issues", projectId] });
      const previousIssues = queryClient.getQueryData(["issues", projectId]);
      
      queryClient.setQueryData(["issues", projectId], (old: Issue[]) => {
        return old.map(issue => issue.id === id ? { ...issue, status } : issue);
      });

      return { previousIssues };
    },
    onError: (err, variables, context) => {
      queryClient.setQueryData(["issues", projectId], context?.previousIssues);
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["issues", projectId] });
    },
  });

  const [items, setItems] = useState<Issue[]>([]);

  // Update local items when issues are fetched
  useEffect(() => {
    if (issues.length > 0) {
      setItems(issues);
    }
  }, [issues]);

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 5,
      },
    }),
    useSensor(KeyboardSensor, {
      coordinateGetter: sortableKeyboardCoordinates,
    })
  );

  const getIssuesByStatus = (status: Status) => {
    return items.filter((item) => item.status === status);
  };

  const findStatusOfItem = (id: string) => {
    const item = items.find((i) => i.id === id);
    return item ? item.status : null;
  };

  const handleDragStart = (event: DragStartEvent) => {
    setActiveId(event.active.id as string);
  };

  const handleDragOver = (event: DragOverEvent) => {
    const { active, over } = event;
    if (!over) return;

    const activeId = active.id as string;
    const overId = over.id as string;

    const activeStatus = findStatusOfItem(activeId);
    const overStatus = COLUMNS.some(c => c.id === overId) 
      ? (overId as Status) 
      : findStatusOfItem(overId);

    if (!activeStatus || !overStatus || activeStatus === overStatus) return;

    setItems((prev) => {
      const activeIdx = prev.findIndex((i) => i.id === activeId);
      const newItems = [...prev];
      if (activeIdx !== -1) {
        newItems[activeIdx] = { ...newItems[activeIdx], status: overStatus };
      }
      return newItems;
    });
  };

  const handleDragEnd = (event: DragEndEvent) => {
    const { active, over } = event;
    if (!over) {
      setActiveId(null);
      return;
    }

    const activeId = active.id as string;
    const overId = over.id as string;

    const activeStatus = findStatusOfItem(activeId);
    const overStatus = COLUMNS.some(c => c.id === overId) 
      ? (overId as Status) 
      : findStatusOfItem(overId);

    if (activeStatus !== overStatus && activeStatus && overStatus) {
      updateStatusMutation.mutate({ id: activeId, status: overStatus });
    }

    if (activeId !== overId) {
      setItems((prev) => {
        const oldIdx = prev.findIndex(i => i.id === activeId);
        const newIdx = prev.findIndex(i => i.id === overId);
        
        if (activeStatus === overStatus && oldIdx !== -1 && newIdx !== -1) {
          return arrayMove(prev, oldIdx, newIdx);
        }
        
        return prev;
      });
    }

    setActiveId(null);
  };

  const dropAnimation: DropAnimation = {
    sideEffects: defaultDropAnimationSideEffects({
      styles: {
        active: {
          opacity: '0.5',
        },
      },
    }),
  };

  if (isLoading) {
    return (
      <div className="h-full flex items-center justify-center">
        <div className="flex flex-col items-center gap-4">
           <div className="w-8 h-8 border-2 border-primary border-t-transparent rounded-full animate-spin" />
           <p className="text-sm text-muted-foreground font-medium">Loading board...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="h-full flex flex-col p-6 min-w-max">
      <DndContext
        sensors={sensors}
        collisionDetection={closestCorners}
        onDragStart={handleDragStart}
        onDragOver={handleDragOver}
        onDragEnd={handleDragEnd}
      >
        <div className="flex gap-6 h-full items-start">
          {COLUMNS.map((col) => (
            <KanbanColumn 
              key={col.id} 
              id={col.id} 
              label={col.label} 
              issues={getIssuesByStatus(col.id)} 
            />
          ))}
        </div>

        <DragOverlay dropAnimation={dropAnimation}>
           {activeId ? (
              <IssueCard 
                issue={items.find(i => i.id === activeId)!} 
                isDragging 
              />
           ) : null}
        </DragOverlay>
      </DndContext>
    </div>
  );
};

