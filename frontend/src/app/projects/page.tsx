"use client";

import React from "react";
import { AppShell } from "@/components/AppShell";
import { Layers, Plus, Search, MoreVertical, Star, Users } from "lucide-react";
import { Button } from "@/components/ui/button";
import { motion } from "framer-motion";
import Link from "next/link";

const PROJECTS = [
  { id: "1", name: "Linear Style PMS", description: "Rebuilding the best PM system in the world.", team: "Product", issues: 45, members: 8, starred: true },
  { id: "2", name: "Mobile App v2", description: "New React Native mobile application.", team: "Engineering", issues: 12, members: 4, starred: true },
  { id: "3", name: "Marketing Site", description: "Marketing and Landing page redesign.", team: "Design", issues: 5, members: 3, starred: false },
  { id: "4", name: "Backend Refactor", description: "Moving to Microservices architecture.", team: "Engineering", issues: 24, members: 5, starred: false },
];

export default function ProjectsPage() {
  return (
    <AppShell>
      <div className="p-8 max-w-6xl mx-auto space-y-8">
        <header className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
          <div className="flex flex-col gap-1">
            <h1 className="text-2xl font-bold tracking-tight">Projects</h1>
            <p className="text-sm text-muted-foreground">Manage all your team projects in one place.</p>
          </div>
          <div className="flex items-center gap-3">
             <div className="relative group flex items-center">
                 <Search className="absolute left-2.5 w-3.5 h-3.5 text-muted-foreground group-focus-within:text-primary transition-colors" />
                 <input 
                   placeholder="Filter projects..." 
                   className="bg-secondary/40 border-none rounded-md px-8 py-1.5 text-xs focus:ring-1 focus:ring-primary/40 focus:bg-secondary/60 transition-all outline-none w-48 sm:w-64"
                 />
             </div>
             <Button variant="primary" size="sm" className="h-8 shadow-lg shadow-primary/20 gap-1.5">
                <Plus className="w-4 h-4" />
                New Project
             </Button>
          </div>
        </header>

        <section className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {PROJECTS.map((project, idx) => (
            <motion.div
              key={project.id}
              initial={{ opacity: 0, y: 15 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: idx * 0.05 }}
              className="group relative flex flex-col p-5 rounded-2xl border border-border/60 bg-[#111112] hover:border-primary/40 transition-all duration-300 shadow-sm hover:shadow-xl hover:shadow-primary/5"
            >
              <div className="flex items-start justify-between gap-2 mb-4">
                 <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-primary/10 to-accent/10 border border-white/5 flex items-center justify-center">
                    <Layers className="w-5 h-5 text-primary" />
                 </div>
                 <div className="flex items-center gap-1.5 opacity-40 group-hover:opacity-100 transition-opacity">
                    <button className={`p-1 rounded hover:bg-secondary/50 transition-colors ${project.starred ? 'text-amber-400 opacity-100' : 'text-muted-foreground'}`}>
                       <Star className="w-4 h-4" fill={project.starred ? "currentColor" : "none"} />
                    </button>
                    <button className="p-1 rounded hover:bg-secondary/50 transition-colors text-muted-foreground">
                       <MoreVertical className="w-4 h-4" />
                    </button>
                 </div>
              </div>

              <div className="mb-6 flex-1">
                 <Link href={`/projects/${project.id}/board`}>
                    <h3 className="font-bold text-foreground group-hover:text-primary transition-colors cursor-pointer mb-1">{project.name}</h3>
                 </Link>
                 <p className="text-xs text-muted-foreground line-clamp-2 leading-relaxed">{project.description}</p>
              </div>

              <div className="flex items-center justify-between pt-4 border-t border-border/10">
                 <div className="flex items-center gap-2">
                    <div className="flex -space-x-2">
                       {[...Array(3)].map((_, i) => (
                          <div key={i} className="w-5 h-5 rounded-full bg-secondary border border-background flex items-center justify-center text-[7px] font-bold">
                             {String.fromCharCode(65 + i)}
                          </div>
                       ))}
                       {project.members > 3 && (
                          <div className="w-5 h-5 rounded-full bg-secondary border border-background flex items-center justify-center text-[7px] font-bold text-muted-foreground">
                             +{project.members - 3}
                          </div>
                       )}
                    </div>
                    <span className="text-[10px] text-muted-foreground font-medium uppercase tracking-wider">{project.team}</span>
                 </div>
                 <div className="flex items-center gap-1 text-[10px] font-bold text-muted-foreground">
                    <span>{project.issues} issues</span>
                 </div>
              </div>
            </motion.div>
          ))}
        </section>
      </div>
    </AppShell>
  );
}
