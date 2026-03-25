"use client";

import React, { useState, useEffect } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { 
  X, 
  Send, 
  MinusCircle, 
  Calendar, 
  Flag, 
  User, 
  Tag as TagIcon,
  Plus,
  MoreHorizontal
} from "lucide-react";
import { Button } from "@/components/ui/button";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { issueService } from "@/lib/api";

const taskSchema = z.object({
  title: z.string().min(1, "Title is required"),
  description: z.string().default(""),
  priority: z.enum(["low", "medium", "high", "urgent"]),
  status: z.enum(["todo", "in_progress", "done", "backlog"]),
  projectId: z.string().min(1, "Project is required"),
});

type TaskFormData = z.infer<typeof taskSchema>;

interface NewTaskModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export const NewTaskModal: React.FC<NewTaskModalProps> = ({ isOpen, onClose }) => {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const { register, handleSubmit, formState: { errors }, reset } = useForm<any>({
    resolver: zodResolver(taskSchema) as any,
    defaultValues: {
      priority: "medium",
      status: "todo",
      projectId: "78d9b1a1-f761-4601-9f90-1c5c6f09bf21",
    }
  });

  const onSubmit = async (data: any) => {
    try {
      setIsSubmitting(true);
      const payload = {
        ...data,
        projectId: data.projectId || "78d9b1a1-f761-4601-9f90-1c5c6f09bf21", // Hardcoded fallback for demo if needed
      };
      
      await issueService.create(payload);
      
      reset();
      onClose();
      // Optional: Refresh data using React Query
    } catch (error) {
      console.error("Failed to create issue:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <AnimatePresence>
      {isOpen && (
        <div className="fixed inset-0 z-[100] flex items-center justify-center p-4">
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="absolute inset-0 bg-background/60 backdrop-blur-md"
            onClick={onClose}
          />
          
          <motion.div
            layoutId="new-task-modal"
            initial={{ opacity: 0, scale: 0.95, y: 15 }}
            animate={{ opacity: 1, scale: 1, y: 0 }}
            exit={{ opacity: 0, scale: 0.95, y: 15 }}
            transition={{ type: "spring", damping: 25, stiffness: 300 }}
            className="relative w-full max-w-[640px] bg-[#111112] border border-border/80 rounded-xl shadow-2xl overflow-hidden flex flex-col"
          >
            <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col">
              <div className="flex items-center justify-between p-3 border-b border-border/10 bg-secondary/10">
                <div className="flex items-center gap-2 px-1">
                  <div className="w-4 h-4 rounded bg-primary text-white flex items-center justify-center text-[8px] font-bold">L</div>
                  <span className="text-[10px] font-semibold text-muted-foreground uppercase tracking-wider">New Issue</span>
                </div>
                <button 
                  type="button" 
                  onClick={onClose}
                  className="p-1 hover:bg-secondary/40 rounded transition-colors text-muted-foreground hover:text-foreground"
                >
                  <X className="w-3.5 h-3.5" />
                </button>
              </div>

              <div className="p-5 space-y-4">
                <div className="space-y-1">
                  <input
                    {...register("title")}
                    autoFocus
                    placeholder="Issue Title"
                    className="w-full bg-transparent border-none focus:ring-0 text-lg font-semibold placeholder:text-muted-foreground/40 text-foreground"
                  />
                  {errors.title && <p className="text-xs text-destructive">{errors.title.message as string}</p>}
                </div>

                <div>
                  <textarea
                    {...register("description")}
                    placeholder="Add description... (supports markdown)"
                    rows={4}
                    className="w-full bg-transparent border-none focus:ring-0 text-sm py-0 placeholder:text-muted-foreground/30 text-foreground/80 resize-none custom-scrollbar"
                  />
                </div>

                <div className="flex flex-wrap items-center gap-2 pt-2">
                   {/* priority */}
                   <div className="flex items-center gap-1.5 px-2.5 py-1 rounded bg-secondary/50 border border-border/30 hover:bg-secondary/70 transition-colors cursor-pointer text-[12px] font-medium text-muted-foreground hover:text-foreground group">
                       <Flag className="w-3 h-3 group-hover:text-amber-500 transition-colors" />
                       <span>Set Priority</span>
                   </div>

                   <div className="flex items-center gap-1.5 px-2.5 py-1 rounded bg-secondary/50 border border-border/30 hover:bg-secondary/70 transition-colors cursor-pointer text-[12px] font-medium text-muted-foreground hover:text-foreground group">
                       <User className="w-3 h-3 group-hover:text-blue-500 transition-colors" />
                       <span>Assignee</span>
                   </div>

                   <div className="flex items-center gap-1.5 px-2.5 py-1 rounded bg-secondary/50 border border-border/30 hover:bg-secondary/70 transition-colors cursor-pointer text-[12px] font-medium text-muted-foreground hover:text-foreground group">
                       <MinusCircle className="w-3 h-3 group-hover:text-emerald-500 transition-colors" />
                       <span>Status</span>
                   </div>

                   <div className="flex items-center gap-1.5 px-2.5 py-1 rounded bg-secondary/50 border border-border/30 hover:bg-secondary/70 transition-colors cursor-pointer text-[12px] font-medium text-muted-foreground hover:text-foreground group">
                       <Calendar className="w-3 h-3 group-hover:text-rose-400 transition-colors" />
                       <span>Due Date</span>
                   </div>

                   <div className="flex items-center gap-1.5 px-2.5 py-1 rounded bg-secondary/50 border border-border/30 hover:bg-secondary/70 transition-colors cursor-pointer text-[12px] font-medium text-muted-foreground hover:text-foreground group">
                       <TagIcon className="w-3 h-3" />
                       <span>Labels</span>
                   </div>

                   <div className="ml-auto flex items-center gap-2">
                       <button type="button" className="p-1.5 hover:bg-secondary/40 rounded-md transition-colors text-muted-foreground">
                          <MoreHorizontal className="w-4 h-4" />
                       </button>
                   </div>
                </div>
              </div>

              <div className="px-5 py-3 border-t border-border/10 bg-secondary/10 flex items-center justify-between">
                <div className="flex items-center gap-3 text-muted-foreground">
                   <div className="flex items-center gap-1">
                      <kbd className="text-[10px] px-1 bg-secondary rounded border border-border/50 font-bold leading-none">⌘</kbd>
                      <kbd className="text-[10px] px-1 bg-secondary rounded border border-border/50 font-bold leading-none">↵</kbd>
                      <span className="text-[10px] font-medium">Create issue</span>
                   </div>
                </div>

                <div className="flex items-center gap-3">
                   <Button 
                    type="submit" 
                    variant="primary" 
                    size="sm" 
                    className="h-8 px-4 font-semibold text-xs shadow-lg shadow-primary/20"
                   >
                     Create Issue
                   </Button>
                </div>
              </div>
            </form>
          </motion.div>
        </div>
      )}
    </AnimatePresence>
  );
};

