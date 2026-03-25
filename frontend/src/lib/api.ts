import axios from "axios";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || "http://localhost:5140/api";

export const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

// Add a request interceptor to add the auth token to headers
api.interceptors.request.use(
  (config) => {
    const token = typeof window !== "undefined" ? localStorage.getItem("auth-token") : null;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Add a response interceptor to handle token expiration
api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      if (typeof window !== "undefined") {
        localStorage.removeItem("auth-token");
        window.location.href = "/login";
      }
    }
    return Promise.reject(error);
  }
);

// Projects API
export const projectService = {
  getAll: () => api.get("/projects"),
  getById: (id: string) => api.get(`/projects/${id}`),
  create: (data: any) => api.post("/projects", data),
};

// Issues API
export const issueService = {
  getByProject: (projectId: string) => api.get(`/issues/project/${projectId}`),
  getById: (id: string) => api.get(`/issues/${id}`),
  create: (data: any) => api.post("/issues", data),
  update: (id: string, data: any) => api.put(`/issues/${id}`, data),
  changeStatus: (id: string, status: string) => api.patch(`/issues/${id}/status`, `"${status}"`, {
    headers: { 'Content-Type': 'application/json' }
  }),
  assign: (id: string, assigneeId: string) => api.patch(`/issues/${id}/assign`, `"${assigneeId}"`, {
    headers: { 'Content-Type': 'application/json' }
  }),
  delete: (id: string) => api.delete(`/issues/${id}`),
};
