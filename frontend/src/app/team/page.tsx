"use client";

import React from "react";
import { AppShell } from "@/components/AppShell";
import { motion } from "framer-motion";
import { Users, Mail, Hash, Shield, MoreHorizontal, UserPlus } from "lucide-react";
import { Button } from "@/components/ui/button";

export default function TeamPage() {
  const members = [
    { name: "Ahsan", role: "Owner", email: "ahsan@gmail.com", avatar: "A", status: "Active" },
    { name: "Sara", role: "Contributor", email: "sara@gmail.com", avatar: "SK", status: "Idle" },
    { name: "Jerry", role: "Admin", email: "jerry@gmail.com", avatar: "JD", status: "Active" },
    { name: "Emily ", role: "Contributor", email: "emily@gmail.com", avatar: "ER", status: "Active" },
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
              Team Management
            </motion.h1>
            <p className="text-sm text-muted-foreground font-medium">Manage your team members and their roles across the organization.</p>
          </div>
          <Button className="bg-primary hover:bg-primary/90 text-white rounded-xl px-4 py-2 font-semibold shadow-lg shadow-primary/20 flex items-center gap-2 transition-all active:scale-95">
             <UserPlus className="w-4 h-4" />
             Invite Member
          </Button>
        </header>

        <section className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
           {members.map((member, idx) => (
              <motion.div 
                key={member.email}
                initial={{ opacity: 0, scale: 0.95 }}
                animate={{ opacity: 1, scale: 1 }}
                transition={{ delay: idx * 0.05 }}
                className="p-6 rounded-2xl border border-border/60 bg-[#111112] hover:border-primary/30 transition-all duration-500 overflow-hidden relative group"
              >
                  <div className="absolute top-0 right-0 p-4 opacity-0 group-hover:opacity-100 transition-opacity">
                     <button className="text-muted-foreground hover:text-foreground">
                        <MoreHorizontal className="w-4 h-4" />
                     </button>
                  </div>
                  
                  <div className="flex flex-col items-center text-center space-y-4">
                      <div className="w-20 h-20 rounded-3xl bg-gradient-to-br from-primary/20 to-accent/20 flex items-center justify-center text-xl font-bold text-white shadow-xl ring-1 ring-white/5 relative overflow-hidden group">
                         <div className="absolute inset-0 bg-primary/20 scale-0 group-hover:scale-100 transition-transform duration-500 rounded-full blur-2xl" />
                         {member.avatar}
                         <div className={`absolute bottom-0 right-0 w-4 h-4 rounded-full border-4 border-[#111112] ${member.status === "Active" ? "bg-green-500" : "bg-yellow-500"}`} />
                      </div>
                      
                      <div className="space-y-1">
                         <h3 className="font-bold text-lg text-foreground">{member.name}</h3>
                         <div className="flex items-center justify-center gap-1.5 px-2.5 py-0.5 rounded-full bg-secondary/30 border border-border/20 text-[10px] font-bold uppercase tracking-[0.1em] text-muted-foreground">
                            {member.role === "Owner" || member.role === "Admin" ? <Shield className="w-3 h-3 text-primary" /> : <Hash className="w-3 h-3" />}
                            {member.role}
                         </div>
                      </div>

                      <div className="w-full pt-4 mt-4 border-t border-border/10 space-y-3">
                         <div className="flex items-center gap-2 text-xs text-muted-foreground/60 font-medium">
                            <Mail className="w-3.5 h-3.5" />
                            <span className="truncate">{member.email}</span>
                         </div>
                      </div>

                      <Button variant="outline" className="w-full rounded-xl border-border/50 text-[11px] font-bold uppercase tracking-widest hover:bg-secondary/40 h-10">
                         View Profile
                      </Button>
                  </div>
              </motion.div>
           ))}
        </section>
      </div>
    </AppShell>
  );
}
