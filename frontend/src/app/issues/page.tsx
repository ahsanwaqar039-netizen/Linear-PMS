"use client";

import React from "react";
import { AppShell } from "@/components/AppShell";
import { motion } from "framer-motion";
import { CheckCircle2, Search, Filter, Plus, Clock, LayoutGrid, List } from "lucide-react";
import { Button } from "@/components/ui/button";

export default function MyIssuesPage() {
  const issues = [
    { id: "LIN-101", title: "Implement JWT Authentication", status: "In Progress", priority: "High", team: "Engineering" },
    { id: "LIN-102", title: "Design System Refinement", status: "Todo", priority: "Medium", team: "Design" },
    { id: "LIN-103", title: "Setup PostgreSQL Database", status: "Done", priority: "High", team: "Engineering" },
  ];

  return (
    <AppShell>
      <div className="p-8 max-w-6xl mx-auto space-y-10">
        <header className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
          <div className="space-y-1">
            <motion.h1 
              initial={{ opacity: 0, x: -10 }}
              animate={{ opacity: 1, x: 0 }}
              className="text-2xl font-bold tracking-tight bg-gradient-to-r from-white to-white/60 bg-clip-text text-transparent"
            >
              My Issues
            </motion.h1>
            <p className="text-sm text-muted-foreground font-medium">Manage and track your assigned work.</p>
          </div>
          <Button className="bg-primary hover:bg-primary/90 text-white rounded-xl px-4 py-2 font-semibold shadow-lg shadow-primary/20 flex items-center gap-2 group transition-all active:scale-95">
             <Plus className="w-4 h-4" />
             New Issue
          </Button>
        </header>

        <section className="space-y-6">
           <div className="flex items-center gap-4 bg-secondary/10 p-2 rounded-xl border border-border/40">
              <div className="relative flex-1 group">
                 <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-muted-foreground transition-colors group-focus-within:text-primary" />
                 <input 
                   type="text" 
                   placeholder="Search issues..." 
                   className="w-full bg-secondary/40 border-none rounded-lg pl-10 pr-4 py-2 text-sm focus:ring-1 focus:ring-primary/40 focus:outline-none transition-all"
                 />
              </div>
              <Button variant="outline" className="rounded-lg h-9 border-border/50 bg-secondary/20 flex items-center gap-2 px-3 text-xs font-bold uppercase tracking-wider">
                 <Filter className="w-3.5 h-3.5" />
                 Filter
              </Button>
              <div className="flex items-center bg-secondary/30 p-1 rounded-lg border border-border/30">
                 <button className="p-1.5 rounded-md bg-secondary text-foreground shadow-sm">
                    <List className="w-4 h-4" />
                 </button>
                 <button className="p-1.5 rounded-md text-muted-foreground hover:bg-secondary/20 transition-colors">
                    <LayoutGrid className="w-4 h-4" />
                 </button>
              </div>
           </div>

           <div className="rounded-2xl border border-border/60 bg-[#0c0c0d] overflow-hidden shadow-sm">
              <div className="divide-y divide-border/30">
                 {issues.map((issue, idx) => (
                    <motion.div 
                      key={issue.id}
                      initial={{ opacity: 0, y: 10 }}
                      animate={{ opacity: 1, y: 0 }}
                      transition={{ delay: 0.1 + idx * 0.05 }}
                      className="p-5 hover:bg-secondary/10 transition-all duration-300 group cursor-pointer flex items-center gap-6"
                    >
                        <div className="flex-1 flex items-center gap-4">
                           <div className={`w-2 h-2 rounded-full ${issue.status === "In Progress" ? "bg-yellow-400" : issue.status === "Done" ? "bg-green-400" : "bg-blue-400"}`} />
                           <div className="flex flex-col gap-0.5 min-w-0">
                               <div className="flex items-center gap-2">
                                  <span className="text-xs font-black tracking-widest text-muted-foreground/60 uppercase">{issue.id}</span>
                                  <h4 className="font-bold text-foreground truncate max-w-[400px] group-hover:text-primary transition-colors">{issue.title}</h4>
                               </div>
                               <div className="flex items-center gap-3 mt-1">
                                  <span className="text-[10px] font-bold text-muted-foreground/40 uppercase tracking-tighter bg-secondary/30 px-1.5 py-0.5 rounded border border-border/20">{issue.team}</span>
                                  <div className="flex items-center gap-1.5 opacity-40">
                                     <Clock className="w-3 h-3" />
                                     <span className="text-[10px] font-bold">Updated 2h ago</span>
                                  </div>
                               </div>
                           </div>
                        </div>

                        <div className="flex items-center gap-4 shrink-0">
                            <div className="hidden sm:flex items-center gap-1 bg-secondary/20 px-2 py-1 rounded-md border border-border/30">
                               <span className={`text-[10px] font-black uppercase tracking-widest ${issue.priority === "High" ? "text-red-400" : "text-yellow-400"}`}>
                                  {issue.priority}
                               </span>
                            </div>
                            <div className="w-8 h-8 rounded-full bg-secondary/80 flex items-center justify-center text-[10px] font-bold text-muted-foreground group-hover:bg-primary group-hover:text-white transition-all shadow-inner border border-white/5">
                               JD
                            </div>
                        </div>
                    </motion.div>
                 ))}
              </div>
              <div className="p-3 bg-secondary/10 border-t border-border/40 text-center">
                 <button className="text-[11px] font-bold text-muted-foreground hover:text-foreground transition-colors uppercase tracking-[0.2em] py-2">View Archive</button>
              </div>
           </div>
        </section>
      </div>
    </AppShell>
  );
}
