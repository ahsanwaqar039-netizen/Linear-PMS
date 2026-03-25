"use client";

import React from "react";
import { useDroppable } from "@dnd-kit/core";
import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { IssueCard } from "@/components/IssueCard";
import { Status, Issue } from "./KanbanBoard";
import { Plus, MoreHorizontal } from "lucide-react";
import { useUIStore } from "@/lib/store";

interface ColumnProps {
  id: Status;
  label: string;
  issues: Issue[];
}

export const KanbanColumn: React.FC<ColumnProps> = ({ id, label, issues }) => {
  const { openNewTaskModal } = useUIStore();
  const { setNodeRef } = useDroppable({
    id: id,
  });

  return (
    <div className="flex flex-col w-[300px] shrink-0 h-full">
      <div className="flex items-center justify-between mb-2 px-2">
        <div className="flex items-center gap-2">
           <div className={`w-2.5 h-2.5 rounded-full ${
               id === 'todo' ? 'bg-blue-400' : 
               id === 'in_progress' ? 'bg-yellow-400' :
               id === 'done' ? 'bg-green-400' : 'bg-gray-400'
           }`} />
           <span className="text-sm font-semibold text-foreground/80 lowercase capitalize">{label}</span>
           <span className="text-[11px] font-bold text-muted-foreground bg-secondary/30 px-1.5 py-0.5 rounded leading-none">
             {issues.length}
           </span>
        </div>
        <div className="flex items-center gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
           <button className="p-1 hover:bg-secondary/40 rounded transition-colors text-muted-foreground hover:text-foreground">
              <Plus className="w-3.5 h-3.5" />
           </button>
           <button className="p-1 hover:bg-secondary/40 rounded transition-colors text-muted-foreground hover:text-foreground">
              <MoreHorizontal className="w-3.5 h-3.5" />
           </button>
        </div>
      </div>

      <div 
        ref={setNodeRef}
        className="flex-1 min-h-[500px] flex flex-col gap-2 rounded-xl group/col transition-colors p-2 -mx-2 bg-transparent hover:bg-secondary/10 overflow-y-auto custom-scrollbar"
      >
        <SortableContext items={issues.map(i => i.id)} strategy={verticalListSortingStrategy}>
          {issues.map((issue) => (
            <IssueCard key={issue.id} issue={issue} />
          ))}
        </SortableContext>
        
        <button 
          onClick={openNewTaskModal}
          className="w-full h-8 flex items-center justify-center border border-dashed border-border/40 rounded-lg text-muted-foreground/40 hover:text-muted-foreground/80 hover:border-primary/50 hover:bg-primary/5 transition-all text-[11px] font-medium opacity-0 group-hover/col:opacity-100 mt-2"
        >
           <Plus className="w-3 h-3 mr-1" />
           Add Issue
        </button>
      </div>
    </div>
  );
};
