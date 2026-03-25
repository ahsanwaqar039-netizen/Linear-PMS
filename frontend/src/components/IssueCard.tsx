"use client";

import React from "react";
import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { Issue } from "./KanbanBoard";
import { 
  CheckCircle2, 
  Circle, 
  Clock, 
  AlertTriangle, 
  ArrowUp, 
  ArrowRight,
  User,
  MoreVertical
} from "lucide-react";
import { motion } from "framer-motion";

interface IssueCardProps {
  issue: Issue;
  isDragging?: boolean;
}

export const IssueCard: React.FC<IssueCardProps> = ({ issue, isDragging }) => {
  const {
    attributes,
    listeners,
    setNodeRef,
    transform,
    transition,
    isOver,
    active
  } = useSortable({
    id: issue.id,
  });

  const style: React.CSSProperties = {
    transform: CSS.Translate.toString(transform),
    transition,
    opacity: isDragging ? 0.4 : 1,
    zIndex: isDragging ? 100 : 1,
  };

  const priorityColors = {
    low: "text-muted-foreground bg-secondary/50",
    medium: "text-blue-500 bg-blue-500/10",
    high: "text-amber-500 bg-amber-500/10",
    urgent: "text-rose-500 bg-rose-500/10",
  };

  const getPriorityIcon = (priority: string) => {
    switch (priority) {
      case "urgent": return <AlertTriangle className="w-3 h-3" />;
      case "high": return <ArrowUp className="w-3 h-3" />;
      case "medium": return <ArrowRight className="w-3 h-3" />;
      default: return <Clock className="w-3 h-3" />;
    }
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      className={`relative p-3 rounded-lg border border-border shadow-md bg-[#111112] transition-all duration-200 cursor-grab active:cursor-grabbing hover:border-primary/40 group overflow-hidden ${
        isOver && !isDragging ? "translate-y-1 scale-[0.98] ring-1 ring-primary/30" : ""
      }`}
      {...attributes}
      {...listeners}
    >
      <div className="flex flex-col gap-2 relative">
          <div className="flex items-center justify-between gap-2 overflow-hidden">
             <div className="flex items-center gap-1.5 overflow-hidden">
                 <span className="text-[10px] font-bold text-muted-foreground uppercase tracking-tight shrink-0">{issue.key}</span>
                 <span className="text-sm font-medium truncate text-foreground/90 group-hover:text-primary transition-colors">{issue.title}</span>
             </div>
             <button className="p-1 hover:bg-secondary/40 rounded transition-colors text-muted-foreground hover:text-foreground opacity-0 group-hover:opacity-100 shrink-0">
                <MoreVertical className="w-3.5 h-3.5" />
             </button>
          </div>

          <div className="flex items-center gap-2 pt-1 border-t border-border/10">
              <div className={`px-1.5 py-0.5 rounded flex items-center gap-1 text-[10px] font-bold uppercase tracking-wider ${priorityColors[issue.priority]}`}>
                  {getPriorityIcon(issue.priority)}
                  {issue.priority}
              </div>

              <div className="flex items-center gap-1 ml-auto text-muted-foreground/40 italic text-[11px] font-medium group-hover:text-muted-foreground/80 transition-colors">
                  <User className="w-3 h-3" />
                  <span>Unassigned</span>
              </div>
          </div>
      </div>
    </div>
  );
};
