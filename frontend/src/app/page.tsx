"use client";

import React from "react";
import { useAuthStore } from "@/lib/store";
import { AppShell } from "@/components/AppShell";
import { motion } from "framer-motion";
import { 
  CheckCircle2, 
  Layers, 
  Clock, 
  TrendingUp, 
  ArrowUpRight,
  Plus
} from "lucide-react";
import { Button } from "@/components/ui/button";
import Link from "next/link";

export default function DashboardPage() {
  const { user } = useAuthStore();

  const stats = [
    { label: "Todo", count: 12, color: "text-blue-400", bg: "bg-blue-400/10", trend: "+5% last week" },
    { label: "In Progress", count: 5, color: "text-yellow-400", bg: "bg-yellow-400/10", trend: "0% change" },
    { label: "Done", count: 24, color: "text-green-400", bg: "bg-green-400/10", trend: "+12% last week" },
    { label: "Backlog", count: 18, color: "text-muted-foreground", bg: "bg-secondary/30", trend: "-2% last week" },
  ];

  return (
    <AppShell>
      <div className="p-8 max-w-6xl mx-auto space-y-10">
        <header className="flex flex-col gap-1.5">
          <motion.h1 
            initial={{ opacity: 0, x: -10 }}
            animate={{ opacity: 1, x: 0 }}
            className="text-2xl font-bold tracking-tight bg-gradient-to-r from-white to-white/60 bg-clip-text text-transparent"
          >
            Good morning, {user?.fullName?.split(" ")[0] || "there"}
          </motion.h1>
          <motion.p 
            initial={{ opacity: 0, x: -10 }}
            animate={{ opacity: 1, x: 0 }}
            transition={{ delay: 0.1 }}
            className="text-sm text-muted-foreground font-medium"
          >
            Here&apos;s a quick overview of what&apos;s happening across your projects today.
          </motion.p>
        </header>

        {/* Stats Grid */}
        <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
          {stats.map((stat, idx) => (
            <motion.div
              key={stat.label}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 0.1 + idx * 0.05 }}
              className="p-5 rounded-2xl border border-border/60 bg-secondary/5 hover:bg-secondary/15 transition-all duration-300 group cursor-pointer shadow-sm hover:shadow-xl hover:shadow-primary/5 active:scale-[0.98]"
            >
              <div className="flex items-center justify-between mb-4">
                 <div className={`p-2 rounded-lg ${stat.bg}`}>
                    <CheckCircle2 className={`w-4 h-4 ${stat.color}`} />
                 </div>
                 <div className="flex items-center gap-1 text-[10px] font-bold text-muted-foreground/60 transition-colors group-hover:text-foreground">
                    <span>{stat.trend}</span>
                    <TrendingUp className="w-3 h-3" />
                 </div>
              </div>
              <div className="space-y-1">
                <span className="text-3xl font-bold tracking-tight">{stat.count}</span>
                <p className="text-sm font-semibold text-muted-foreground group-hover:text-foreground transition-colors">{stat.label}</p>
              </div>
            </motion.div>
          ))}
        </section>

        {/* Recent Activity & Projects */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
           <section className="lg:col-span-2 space-y-4">
              <div className="flex items-center justify-between px-1">
                 <h3 className="text-sm font-bold tracking-wider uppercase text-muted-foreground/60">Active Projects</h3>
                 <Link href="/projects" className="text-xs font-semibold text-primary hover:underline transition-all">See All</Link>
              </div>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                 {[
                   { id: "78d9b1a1-f761-4601-9f90-1c5c6f09bf21", name: "Project Alpha", tech: "Mobile App", progress: 65, status: "Active" },
                   { id: "78d9b1a1-f761-4601-9f90-1c5c6f09bf21", name: "Website Redesign", tech: "Branding", progress: 40, status: "In Planning" },
                 ].map((proj, idx) => (
                    <Link 
                      key={proj.name}
                      href={`/projects/${proj.id}/board`}
                      className="p-5 rounded-2xl border border-border/80 bg-[#111112] hover:border-primary/40 transition-all duration-300 group cursor-pointer relative overflow-hidden block"
                    >
                        <div className="flex items-center justify-between mb-4">
                           <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-primary/20 to-accent/20 flex items-center justify-center border border-white/5 shadow-inner">
                              <Layers className="w-5 h-5 text-primary" />
                           </div>
                           <ArrowUpRight className="w-4 h-4 text-muted-foreground/40 group-hover:text-primary transition-colors" />
                        </div>
                        <h4 className="font-bold text-foreground mb-1">{proj.name}</h4>
                        <p className="text-xs text-muted-foreground mb-4 font-medium">{proj.tech} &bull; {proj.status}</p>
                        
                        <div className="w-full h-1 bg-secondary/30 rounded-full overflow-hidden">
                           <motion.div 
                             initial={{ width: 0 }}
                             animate={{ width: `${proj.progress}%` }}
                             transition={{ duration: 1, delay: 0.5 + idx * 0.1 }}
                             className="h-full bg-primary" 
                           />
                        </div>
                    </Link>
                 ))}
              </div>
           </section>

           <section className="space-y-4">
              <div className="flex items-center justify-between px-1">
                 <h3 className="text-sm font-bold tracking-wider uppercase text-muted-foreground/60">Your Feed</h3>
              </div>
              <div className="rounded-2xl border border-border/80 bg-[#0c0c0d] overflow-hidden shadow-2xl">
                 <div className="divide-y divide-border/40">
                    {[
                      { user: "Sarah K.", action: "moved to In Progress", target: "LIN-102 Design System", time: "2h ago", avatar: "SK" },
                      { user: "James D.", action: "completed", target: "LIN-105 Setup PG", time: "4h ago", avatar: "JD" },
                      { user: "Me", action: "commented on", target: "LIN-101 JWT Auth", time: "5h ago", avatar: "JD" },
                      { user: "Sarah K.", action: "created new issue", target: "LIN-108 Dark mode", time: "6h ago", avatar: "SK" },
                    ].map((item, idx) => (
                      <div key={idx} className="p-4 hover:bg-secondary/20 transition-colors flex items-start gap-3 group">
                        <div className="w-7 h-7 rounded-lg bg-secondary/50 flex items-center justify-center text-[9px] font-bold text-muted-foreground group-hover:bg-primary/20 group-hover:text-primary transition-all">
                           {item.avatar}
                        </div>
                        <div className="flex flex-col gap-0.5">
                           <p className="text-xs text-foreground/90 leading-normal">
                              <span className="font-bold text-foreground">{item.user}</span> {item.action} 
                              <span className="font-semibold block mt-0.5 text-primary/80">{item.target}</span>
                           </p>
                           <span className="text-[10px] text-muted-foreground/60">{item.time}</span>
                        </div>
                      </div>
                    ))}
                 </div>
                 <div className="p-3 bg-secondary/10 border-t border-border/40 text-center">
                    <button className="text-[11px] font-bold text-muted-foreground hover:text-foreground transition-colors uppercase tracking-widest">Show older activity</button>
                 </div>
              </div>
           </section>
        </div>
      </div>
    </AppShell>
  );
}

