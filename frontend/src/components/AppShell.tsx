"use client";

import React, { useState, useEffect } from "react";
import { Sidebar } from "@/components/Sidebar";
import { Navbar } from "@/components/Navbar";
import { NewTaskModal } from "@/components/NewTaskModal";
import { motion, AnimatePresence } from "framer-motion";

import { useAuthStore, useUIStore } from "@/lib/store";

export const AppShell = ({ children }: { children: React.ReactNode }) => {
  const { isNewTaskModalOpen, openNewTaskModal, closeNewTaskModal } = useUIStore();

  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      const target = e.target as HTMLElement;
      if (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA' || target.isContentEditable) return;

      if (e.key.toLowerCase() === 'c') {
        e.preventDefault();
        openNewTaskModal();
      }

      if ((e.metaKey || e.ctrlKey) && e.key.toLowerCase() === 'k') {
        e.preventDefault();
        const searchInput = document.querySelector('input[placeholder*="Search"]') as HTMLInputElement;
        if (searchInput) searchInput.focus();
      }
    };

    window.addEventListener("keydown", handleKeyDown);
    return () => window.removeEventListener("keydown", handleKeyDown);
  }, []);

  return (
    <div className="flex h-screen overflow-hidden bg-background">
      <Sidebar />
      <main className="flex-1 flex flex-col relative overflow-hidden">
        <Navbar onOpenNewTask={openNewTaskModal} />
        <div className="flex-1 overflow-y-auto custom-scrollbar relative">
           <AnimatePresence mode="wait">
             <motion.div
               key={Math.random()} 
               initial={{ opacity: 0, y: 10 }}
               animate={{ opacity: 1, y: 0 }}
               exit={{ opacity: 0, y: -10 }}
               transition={{ duration: 0.2, ease: "easeOut" }}
               className="min-h-full"
             >
               {children}
             </motion.div>
           </AnimatePresence>
        </div>
      </main>

      <NewTaskModal 
        isOpen={isNewTaskModalOpen} 
        onClose={closeNewTaskModal} 
      />
    </div>
  );
};
