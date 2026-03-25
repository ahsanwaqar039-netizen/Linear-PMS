import { create } from "zustand";
import { persist } from "zustand/middleware";

interface User {
  email: string;
  fullName: string;
}

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  setAuth: (user: User, token: string) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      user: null,
      token: null,
      isAuthenticated: false,
      setAuth: (user, token) => {
        if (typeof window !== "undefined") {
          localStorage.setItem("auth-token", token);
        }
        set({ user, token, isAuthenticated: true });
      },
      logout: () => {
        if (typeof window !== "undefined") {
          localStorage.removeItem("auth-token");
          document.cookie = "auth-token=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
        set({ user: null, token: null, isAuthenticated: false });
      },
    }),
    {
      name: "auth-storage",
    }
  )
);
interface UIState {
  isNewTaskModalOpen: boolean;
  openNewTaskModal: () => void;
  closeNewTaskModal: () => void;
}

export const useUIStore = create<UIState>()((set) => ({
  isNewTaskModalOpen: false,
  openNewTaskModal: () => set({ isNewTaskModalOpen: true }),
  closeNewTaskModal: () => set({ isNewTaskModalOpen: false }),
}));
