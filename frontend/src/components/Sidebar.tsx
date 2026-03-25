"use client";

import React from "react";
import { useAuthStore } from "@/lib/store";
import { 
  LayoutDashboard, 
  Settings, 
  Users, 
  CheckCircle2, 
  Layers, 
  LogOut,
  ChevronRight,
  PlusCircle,
  Hash,
  MessageSquare,
  Search,
  Bell
} from "lucide-react";
import { useRouter, usePathname } from "next/navigation";
import Link from "next/link";
import { motion } from "framer-motion";

export const Sidebar = () => {
  const { user, logout } = useAuthStore();
  const router = useRouter();
  const pathname = usePathname();

  const handleLogout = () => {
    logout();
    router.push("/login");
  };

  const menuItems = [
    { icon: LayoutDashboard, label: "Dashboard", href: "/" },
    { icon: Layers, label: "Projects", href: "/projects" },
    { icon: CheckCircle2, label: "My Issues", href: "/issues" },
    { icon: Users, label: "Team", href: "/team" },
    { icon: Settings, label: "Settings", href: "/settings" },
  ];

  const teams = [
    { name: "Engineering", id: "eng", projectId: "78d9b1a1-f761-4601-9f90-1c5c6f09bf21" },
    { name: "Product", id: "prod", projectId: "78d9b1a1-f761-4601-9f90-1c5c6f09bf21" },
    { name: "Design", id: "design", projectId: "78d9b1a1-f761-4601-9f90-1c5c6f09bf21" },
  ];

  return (
    <aside className="w-64 border-r border-border bg-[#080809] flex flex-col h-full select-none shrink-0 group">

      <div className="p-4">
        <Link href="/" className="flex items-center gap-2 mb-6 px-2 hover:opacity-80 transition-opacity">
          <div className="w-6 h-6 rounded-md bg-primary flex items-center justify-center text-white font-bold text-xs ring-1 ring-white/10">
            L
          </div>
          <span className="font-semibold text-sm tracking-tight text-foreground/90">Linear PMS</span>
        </Link>

        <div className="space-y-1 mb-6 px-1">
          <button className="w-full flex items-center gap-2 px-2 py-1.5 rounded-md hover:bg-secondary/50 text-muted-foreground hover:text-foreground transition-all duration-200 text-sm">
            <Search className="w-3.5 h-3.5" />
            <span className="flex-1 text-left">Search</span>
            <kbd className="text-[10px] bg-secondary px-1 rounded border border-border/50 opacity-50">⌘K</kbd>
          </button>
          <button className="w-full flex items-center gap-2 px-2 py-1.5 rounded-md hover:bg-secondary/50 text-muted-foreground hover:text-foreground transition-all duration-200 text-sm">
            <Bell className="w-3.5 h-3.5" />
            <span className="flex-1 text-left">Inbox</span>
            <span className="w-4 h-4 rounded-full bg-primary/20 text-primary text-[10px] flex items-center justify-center font-bold">2</span>
          </button>
        </div>

        <nav className="space-y-0.5 px-1 overflow-y-auto max-h-[60vh] custom-scrollbar">
          {menuItems.map((item) => {
            const isActive = pathname === item.href;
            return (
              <Link
                key={item.href}
                href={item.href}
                className={`flex items-center gap-2.5 px-2 py-1.5 rounded-md transition-all duration-200 text-sm ${
                  isActive 
                    ? "bg-secondary text-foreground font-medium shadow-sm" 
                    : "text-muted-foreground hover:bg-secondary/30 hover:text-foreground"
                }`}
              >
                <item.icon className={`w-4 h-4 ${isActive ? "text-primary" : "opacity-60"}`} />
                <span>{item.label}</span>
              </Link>
            );
          })}

          <div className="pt-4 pb-2">
             <span className="px-2 text-[11px] font-semibold text-muted-foreground uppercase tracking-wider">Your Teams</span>
          </div>

          {teams.map((team) => (
            <Link
              key={team.id}
              href={`/projects/${team.projectId}/board`}
              className="flex items-center gap-2.5 px-2 py-1.5 rounded-md text-muted-foreground hover:bg-secondary/30 hover:text-foreground transition-all duration-200 text-sm"
            >
              <Hash className="w-4 h-4 opacity-60" />
              <span>{team.name}</span>
            </Link>
          ))}

          <button className="w-full flex items-center gap-2.5 px-2 py-1.5 rounded-md text-muted-foreground hover:bg-secondary/30 hover:text-foreground transition-all duration-200 text-sm">
            <PlusCircle className="w-4 h-4 opacity-60" />
            <span>Join or Create Team</span>
          </button>
        </nav>
      </div>

      <div className="mt-auto p-4 border-t border-border bg-[#0a0a0b]/50">
        <div className="flex items-center justify-between p-2 rounded-lg hover:bg-secondary/30 transition-colors cursor-pointer">
          <div className="flex items-center gap-3 overflow-hidden">
            <div className="w-8 h-8 rounded-full bg-gradient-to-br from-primary to-accent-foreground flex items-center justify-center text-[10px] font-bold text-white shadow-lg ring-1 ring-white/10 overflow-hidden">
              {user?.fullName?.split(" ").map(n => n[0]).join("") || "U"}
            </div>
            <div className="flex flex-col overflow-hidden">
              <span className="text-xs font-semibold truncate text-foreground">{user?.fullName || "User Name"}</span>
              <span className="text-[10px] text-muted-foreground truncate">{user?.email || "user@example.com"}</span>
            </div>
          </div>
          <button 
            onClick={handleLogout}
            className="p-1 px-1.5 rounded hover:bg-destructive/10 hover:text-destructive text-muted-foreground/60 transition-colors"
            title="Logout"
          >
            <LogOut className="w-3.5 h-3.5" />
          </button>
        </div>
      </div>
    </aside>
  );
};
