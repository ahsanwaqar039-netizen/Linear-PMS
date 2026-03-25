"use client";

import React from "react";
import { Plus, ChevronLeft, ChevronRight, Menu, Bell, Search, Command } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useAuthStore } from "@/lib/store";
import { usePathname } from "next/navigation";
import Link from "next/link";
import { 
  DropdownMenu, 
  DropdownMenuContent, 
  DropdownMenuItem, 
  DropdownMenuSeparator, 
  DropdownMenuTrigger 
} from "@/components/ui/dropdown-menu";

export const Navbar = ({ onOpenNewTask }: { onOpenNewTask: () => void }) => {
  const pathname = usePathname();
  const notifications = [
    { id: 1, title: "Issue Assigned", description: "You've been assigned LIN-101", time: "2m ago", unread: true },
    { id: 2, title: "New Comment", description: "Sarah commented on LIN-105", time: "1h ago", unread: true },
  ];

  return (
    <header className="h-12 border-b border-border flex items-center justify-between px-4 bg-background/50 backdrop-blur-md sticky top-0 z-50">
      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2 group cursor-pointer">
          <Menu className="w-4 h-4 text-muted-foreground hover:text-foreground transition-colors mr-2 cursor-pointer" />
          <nav className="flex items-center gap-2 text-sm text-muted-foreground h-full">
             <Link href="/" className="font-medium text-foreground hover:text-primary transition-colors">Dashboard</Link>
             {pathname.includes("/projects") && (
                 <>
                   <ChevronRight className="w-3.5 h-3.5 opacity-50" />
                   <span className="font-semibold text-foreground/90">Project Alpha</span>
                 </>
             )}
          </nav>
        </div>
      </div>

      <div className="flex items-center gap-3">
        <div className="relative group hidden sm:flex items-center">
            <Search className="absolute left-2.5 w-3.5 h-3.5 text-muted-foreground group-focus-within:text-primary transition-colors" />
            <input 
              type="text" 
              placeholder="Search or ask..." 
              className="bg-secondary/40 border-none rounded-md px-8 py-1 text-xs focus:ring-1 focus:ring-primary/40 focus:bg-secondary/60 transition-all outline-none w-48 group-hover:bg-secondary/60 group-hover:w-56"
            />
            <div className="absolute right-2.5 flex items-center gap-0.5 opacity-40 group-focus-within:opacity-100">
                <Command className="w-2.5 h-2.5" />
                <span className="text-[10px] font-bold">K</span>
            </div>
        </div>

        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <button className="p-1.5 hover:bg-secondary/80 rounded-md transition-colors relative">
                <Bell className="w-4 h-4 text-muted-foreground hover:text-foreground transition-colors" />
                <span className="absolute top-1 right-1 w-1.5 h-1.5 bg-primary rounded-full ring-1 ring-background" />
            </button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end" className="w-80 bg-[#111112] border-border/80 shadow-2xl p-2 rounded-xl">
             <div className="flex items-center justify-between px-3 py-2 mb-1">
                <span className="text-xs font-bold text-foreground">Notifications</span>
                <button className="text-[10px] text-primary hover:underline font-semibold">Mark all read</button>
             </div>
             <DropdownMenuSeparator className="bg-border/10" />
             <div className="max-h-[320px] overflow-y-auto custom-scrollbar pt-1">
                {notifications.map((n) => (
                   <DropdownMenuItem key={n.id} className="flex flex-col items-start gap-1 p-3 rounded-lg hover:bg-secondary/40 focus:bg-secondary/40 transition-colors cursor-pointer border-none outline-none mb-1 group">
                      <div className="flex items-center justify-between w-full">
                         <span className={`text-xs font-bold ${n.unread ? 'text-foreground' : 'text-muted-foreground'}`}>{n.title}</span>
                         <span className="text-[10px] text-muted-foreground/60">{n.time}</span>
                      </div>
                      <p className="text-[11px] text-muted-foreground group-hover:text-foreground/80 transition-colors line-clamp-1">{n.description}</p>
                   </DropdownMenuItem>
                ))}
             </div>
             <DropdownMenuSeparator className="bg-border/10" />
             <div className="p-2 text-center mt-1">
                <button className="text-[10px] font-bold text-muted-foreground hover:text-foreground transition-colors uppercase tracking-widest">Inbox Settings</button>
             </div>
          </DropdownMenuContent>
        </DropdownMenu>

        <Button 
          size="sm" 
          variant="primary"
          className="h-8 px-3 rounded-md text-xs font-semibold gap-1.5 shadow-lg shadow-primary/10 transition-all hover:scale-[1.02] active:scale-[0.98]"
          onClick={onOpenNewTask}
        >
          <Plus className="w-4 h-4" />
          New Issue
        </Button>
      </div>
    </header>
  );
};
