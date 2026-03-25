"use client";

import React from "react";
import { AppShell } from "@/components/AppShell";
import { motion } from "framer-motion";
import { Settings, User, Bell, Palette, Lock, Globe, Database, HelpCircle, Save } from "lucide-react";
import { Button } from "@/components/ui/button";

export default function SettingsPage() {
  const sections = [
    { icon: User, label: "My Profile", active: true },
    { icon: Bell, label: "Notifications", active: false },
    { icon: Palette, label: "Appearance", active: false },
    { icon: Lock, label: "Security", active: false },
    { icon: Globe, label: "Integrations", active: false },
    { icon: Database, label: "Usage & Billing", active: false },
    { icon: HelpCircle, label: "Support", active: false },
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
              Organization Settings
            </motion.h1>
            <p className="text-sm text-muted-foreground font-medium">Manage your personal and organizational preferences.</p>
          </div>
          <Button className="bg-primary hover:bg-primary/90 text-white rounded-xl px-4 py-2 font-semibold shadow-lg shadow-primary/20 flex items-center gap-2 transition-all active:scale-95 group">
             <Save className="w-4 h-4 transition-transform group-hover:scale-110" />
             Save Changes
          </Button>
        </header>

        <section className="grid grid-cols-1 lg:grid-cols-4 gap-8">
           <aside className="lg:col-span-1 space-y-1">
              <span className="px-2 text-[10px] font-bold text-muted-foreground/60 uppercase tracking-widest block mb-3">General Settings</span>
              {sections.map((section, idx) => (
                 <motion.button 
                   initial={{ opacity: 0, x: -10 }}
                   animate={{ opacity: 1, x: 0 }}
                   transition={{ delay: idx * 0.05 }}
                   key={section.label}
                   className={`w-full flex items-center gap-3 px-3 py-2 rounded-xl text-sm font-semibold transition-all duration-300 ${
                     section.active 
                       ? "bg-secondary text-foreground shadow-sm ring-1 ring-white/5" 
                       : "text-muted-foreground hover:bg-secondary/40 hover:text-foreground"
                   }`}
                 >
                    <section.icon className={`w-4 h-4 ${section.active ? "text-primary" : "opacity-40"}`} strokeWidth={2} />
                    {section.label}
                 </motion.button>
              ))}
           </aside>

           <main className="lg:col-span-3 space-y-8 bg-secondary/10 p-8 rounded-3xl border border-border/40 backdrop-blur-3xl shadow-2xl">
              <div className="space-y-6">
                 <div className="flex items-center gap-6">
                    <div className="w-24 h-24 rounded-3xl bg-gradient-to-br from-primary/30 to-accent/30 flex items-center justify-center text-4xl font-bold shadow-2xl relative group cursor-pointer overflow-hidden border border-white/5">
                        <div className="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center">
                           <span className="text-xs font-bold uppercase tracking-widest text-white">Upload</span>
                        </div>
                        JD
                    </div>
                    <div className="space-y-1">
                       <h3 className="font-bold text-lg">Your ID Avatar</h3>
                       <p className="text-xs text-muted-foreground/60 leading-relaxed max-w-[300px]">Customize your profile picture to represent yourself within the workspace.</p>
                       <div className="flex gap-4 pt-2">
                          <Button variant="outline" className="h-8 text-[10px] font-bold uppercase tracking-widest rounded-lg border-border/50 bg-secondary/20 h-8">Remove Photo</Button>
                          <Button variant="outline" className="h-8 text-[10px] font-bold uppercase tracking-widest rounded-lg border-primary/20 text-primary h-8">Change Avatar</Button>
                       </div>
                    </div>
                 </div>

                 <div className="pt-8 border-t border-border/20 grid grid-cols-1 md:grid-cols-2 gap-8">
                    <div className="space-y-2">
                       <label className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest block ml-1">Full Name</label>
                       <input 
                         type="text" 
                         defaultValue="James Doe"
                         placeholder="Your full name" 
                         className="w-full bg-secondary/40 border border-white/5 rounded-xl px-4 py-2 text-sm focus:ring-1 focus:ring-primary/40 focus:outline-none transition-all placeholder:text-muted-foreground/30 font-semibold"
                       />
                    </div>
                    <div className="space-y-2">
                       <label className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest block ml-1">Display Name</label>
                       <input 
                         type="text" 
                         defaultValue="james.doe"
                         placeholder="Workspace username" 
                         className="w-full bg-secondary/40 border border-white/5 rounded-xl px-4 py-2 text-sm focus:ring-1 focus:ring-primary/40 focus:outline-none transition-all placeholder:text-muted-foreground/30 font-semibold"
                       />
                    </div>
                    <div className="space-y-2 md:col-span-2">
                       <label className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest block ml-1">Biography</label>
                       <textarea 
                         rows={3}
                         placeholder="Write a brief bio about yourself..." 
                         className="w-full bg-secondary/40 border border-white/5 rounded-xl px-4 py-2 text-sm focus:ring-1 focus:ring-primary/40 focus:outline-none transition-all placeholder:text-muted-foreground/30 font-semibold resize-none"
                       />
                    </div>
                 </div>
              </div>
           </main>
        </section>
      </div>
    </AppShell>
  );
}
