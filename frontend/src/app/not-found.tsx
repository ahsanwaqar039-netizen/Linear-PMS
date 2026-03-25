"use client";

import React from "react";
import Link from "next/link";
import { motion } from "framer-motion";
import { Home, ArrowLeft, Ghost } from "lucide-react";
import { Button } from "@/components/ui/button";

export default function NotFound() {
  return (
    <div className="min-h-screen bg-background flex items-center justify-center p-6 select-none overflow-hidden relative">
      <div className="absolute inset-0 overflow-hidden pointer-events-none opacity-20">
         <div className="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-[800px] h-[800px] bg-primary/20 rounded-full blur-[160px]" />
         <div className="absolute top-1/4 left-1/3 w-64 h-64 bg-accent/20 rounded-full blur-[100px]" />
      </div>

      <div className="max-w-md w-full text-center space-y-8 relative z-10">
        <motion.div
           initial={{ opacity: 0, scale: 0.9 }}
           animate={{ opacity: 1, scale: 1 }}
           transition={{ duration: 0.5 }}
           className="relative inline-block"
        >
          <div className="w-32 h-32 rounded-3xl bg-secondary/50 flex items-center justify-center mx-auto border border-white/5 shadow-2xl backdrop-blur-xl">
             <Ghost className="w-16 h-16 text-primary" strokeWidth={1.5} />
          </div>
          <motion.div 
            animate={{ y: [0, -10, 0] }}
            transition={{ repeat: Infinity, duration: 3, ease: "easeInOut" }}
            className="absolute -top-4 -right-4 w-12 h-12 rounded-full bg-accent/40 flex items-center justify-center border border-white/10 backdrop-blur-md shadow-lg"
          >
             <span className="text-xl font-bold italic text-white/80">?</span>
          </motion.div>
        </motion.div>

        <div className="space-y-4">
          <motion.h1 
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.1 }}
            className="text-7xl font-black bg-gradient-to-b from-white to-white/30 bg-clip-text text-transparent tracking-tighter"
          >
            404
          </motion.h1>
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
            className="space-y-2"
          >
            <h2 className="text-2xl font-bold text-foreground">Lost in space?</h2>
            <p className="text-muted-foreground font-medium text-sm leading-relaxed max-w-[280px] mx-auto">
                The page you&apos;re looking for has either drifted out of orbit or never existed.
            </p>
          </motion.div>
        </div>

        <motion.div 
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.3 }}
          className="flex flex-col sm:flex-row items-center justify-center gap-4 pt-4"
        >
          <Button 
            asChild
            variant="outline" 
            className="w-full sm:w-auto px-6 h-11 border-border/50 hover:bg-secondary/50 group rounded-xl transition-all active:scale-95"
          >
            <button onClick={() => window.history.back()} className="flex items-center gap-2">
              <ArrowLeft className="w-4 h-4 transition-transform group-hover:-translate-x-1" />
              Go Back
            </button>
          </Button>
          <Button 
            asChild
            className="w-full sm:w-auto px-6 h-11 bg-primary hover:bg-primary/90 text-white shadow-lg shadow-primary/20 rounded-xl transition-all active:scale-95"
          >
            <Link href="/" className="flex items-center gap-2">
              <Home className="w-4 h-4" />
              Return Home
            </Link>
          </Button>
        </motion.div>

        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.5 }}
          className="pt-12 flex items-center justify-center gap-8 opacity-40 grayscale hover:grayscale-0 transition-all duration-700"
        >
           <div className="text-[10px] font-bold tracking-[0.2em] uppercase text-muted-foreground border-t border-border/30 pt-4 px-2">Linear PMS System</div>
        </motion.div>
      </div>
    </div>
  );
}
