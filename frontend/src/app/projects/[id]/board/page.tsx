"use client";

import React from "react";
import { AppShell } from "@/components/AppShell";
import { KanbanBoard } from "@/components/KanbanBoard";
import { Layers, ChevronRight, Settings, Plus, Star, MoreHorizontal, Bell, Search, Command } from "lucide-react";
import { Button } from "@/components/ui/button";
import { motion } from "framer-motion";

export default function ProjectBoardPage() {
  return (
    <AppShell>
      <div className="flex flex-col h-full overflow-hidden bg-background">
        <header className="h-14 border-b border-border/60 flex items-center justify-between px-6 bg-background/50 backdrop-blur-sm sticky top-0 z-40">
           <div className="flex items-center gap-4">
              <div className="flex items-center gap-2 group cursor-pointer">
                  <div className="w-8 h-8 rounded-lg bg-primary/20 flex items-center justify-center border border-primary/10 shadow-lg shadow-primary/5">
                      <Layers className="w-4 h-4 text-primary" />
                  </div>
                  <div className="flex flex-col">
                      <div className="flex items-center gap-1.5 leading-tight">
                         <span className="text-sm font-bold text-foreground">Linear Style PMS</span>
                         <Star className="w-3.5 h-3.5 text-amber-500 fill-amber-500 shadow-xl" />
                      </div>
                      <span className="text-[10px] text-muted-foreground font-semibold uppercase tracking-wider leading-none">LIN-1 • engineering</span>
                  </div>
              </div>

              <div className="flex items-center gap-2 p-1.5 px-3 rounded-full bg-secondary/30 border border-border/20 text-[11px] font-bold text-muted-foreground hover:bg-secondary/50 transition-colors cursor-pointer group">
                  <div className="w-1.5 h-1.5 rounded-full bg-green-500 animate-pulse group-hover:scale-125 transition-transform" />
                  <span>Public Project</span>
              </div>
           </div>

           <div className="flex items-center gap-4">
               <div className="flex -space-x-2">
                  {[...Array(5)].map((_, i) => (
                      <div key={i} className="w-6 h-6 rounded-full bg-gradient-to-br from-primary/40 to-accent/40 border-2 border-background flex items-center justify-center text-[8px] font-extrabold text-white shadow-lg overflow-hidden ring-1 ring-white/5">
                        {String.fromCharCode(65 + i)}
                      </div>
                  ))}
                  <div className="w-6 h-6 rounded-full bg-secondary/80 border-2 border-background flex items-center justify-center text-[8px] font-black text-muted-foreground shadow-lg hover:scale-110 transition-transform cursor-pointer">
                    +3
                  </div>
               </div>

               <div className="h-4 w-[1px] bg-border/60 hidden sm:block" />

               <div className="flex items-center gap-2">
                  <button className="p-1.5 hover:bg-secondary/40 rounded-md transition-colors text-muted-foreground hover:text-foreground">
                      <Search className="w-4 h-4" />
                  </button>
                  <button className="p-1.5 hover:bg-secondary/40 rounded-md transition-colors text-muted-foreground hover:text-foreground">
                      <Settings className="w-4 h-4" />
                  </button>
                  <button className="p-1.5 hover:bg-secondary/40 rounded-md transition-colors text-muted-foreground hover:text-foreground">
                      <MoreHorizontal className="w-4 h-4" />
                  </button>
               </div>
           </div>
        </header>

        <div className="flex-1 overflow-x-auto overflow-y-hidden bg-[#080809]">
           <KanbanBoard />
        </div>
      </div>
    </AppShell>
  );
}
