# Linear PMS | Project Management System

A high-performance, premium project management system inspired by the **Linear** design language. Built with a modern tech stack focusing on developer velocity and user experience.

![Project Banner](https://images.unsplash.com/photo-1555066931-4365d14bab8c?auto=format&fit=crop&q=80&w=1200&h=400)

## 🚀 Key Features

- **Linear-Inspired UI**: Stunning dark mode with glassmorphism, smooth Framer Motion transitions, and high-quality typography.
- **Kanban Board**: Interactive drag-and-drop task management powered by `@dnd-kit`.
- **Global Search**: Command+K (Ctrl+K) interface for quick navigation across issues and projects.
- **Collaborative Teams**: Manage multiple projects with team-based permissions and roles.
- **Real-Time Feed**: Activity streams to keep track of every change in your workspace.
- **JWT Auth**: Secure authentication and session management.

## 🛠️ Tech Stack

### Frontend
- **Framework**: [Next.js 16](https://nextjs.org/) (App Router)
- **Styling**: [Tailwind CSS v4](https://tailwindcss.com/)
- **State Management**: [Zustand](https://github.com/pmndrs/zustand)
- **Data Fetching**: [TanStack Query v5](https://tanstack.com/query/latest)
- **Animations**: [Framer Motion](https://www.framer.com/motion/)
- **Icons**: [Lucide React](https://lucide.dev/)

### Backend
- **Framework**: [.NET 8.0 Web API](https://dotnet.microsoft.com/)
- **Architecture**: Repository Pattern with DTOs
- **Database**: [PostgreSQL](https://www.postgresql.org/)
- **ORM**: [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- **Documentation**: [Swagger / OpenAPI](https://swagger.io/)
- **Security**: JWT Bearer Authentication & BCrypt

## ⚙️ Getting Started

### Prerequisites
- [Node.js](https://nodejs.org/) (v20 or newer)
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/ahsanwaqar039-netizen/Linear-PMS.git
   cd Linear-PMS
   ```

2. **Setup Backend**:
   - Update `appsettings.json` with your PostgreSQL connection string.
   - Run migrations:
     ```bash
     cd backend
     dotnet ef database update
     dotnet run
     ```

3. **Setup Frontend**:
   - Create `.env.local` based on `.env.example`.
   - Install dependencies and start:
     ```bash
     cd frontend
     npm install
     npm run dev
     ```

## 📂 Project Structure

```text
├── frontend/             # Next.js Application
│   ├── src/app/          # Routes & Pages
│   ├── src/components/   # UI & Shared Components
│   └── src/lib/          # API services & Stores
├── backend/              # .NET Core Web API
│   ├── Controllers/      # API Endpoints
│   ├── Models/           # Database Entities
│   ├── DTOs/             # Data Transfer Objects
│   └── Repositories/     # Data Access Layer
└── .gitignore            # Root-level git rules
```

## 📜 License

Created by **Ahsan Waqar**. Inspired by the beautiful work of the [Linear](https://linear.app) team.
