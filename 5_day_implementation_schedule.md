# 5-Day Sequential Implementation Plan: Linear-Style PMS

## Day 1: Database Setup & Schema Design

**Focus:** PostgreSQL database setup and core data modeling.

- **Step 1:** Setup PostgreSQL database (locally via Docker or pgAdmin).
- **Step 2:** Design the database schema (ER Diagram conceptualization) for Users, Teams, Projects, Issues, and TimeLogs.
- **Step 3:** Define relationships (e.g., One User -> Many Teams, One Project -> Many Issues).
- **Step 4:** Document the initial schema tables and columns to prepare for Entity Framework Core.
- **Step 5:** Create a `docker-compose.yml` file to easily spin up the database container along with any caching layers (e.g., Redis if needed later).

---

## Day 2: Backend Foundation & Authentication (.NET API)

**Focus:** Scaffold the .NET Web API, connect to the database, and build the Auth system.

- **Step 1:** Initialize the `.NET 7/8 Web API` project structure (Controllers, Services, Repositories, DTOs).
- **Step 2:** Install required NuGet packages (`Npgsql.EntityFrameworkCore.PostgreSQL`, `Microsoft.AspNetCore.Authentication.JwtBearer`).
- **Step 3:** Setup Entity Framework Core `DbContext`, configure connection strings, and run the first migrations to create tables in PostgreSQL.
- **Step 4:** Implement JWT Authentication logic (Token generation and validation).
- **Step 5:** Create APIs for User Signup, Login, and basic Team creation endpoints. Secure them with `[Authorize]`.

---

## Day 3: Backend Core Features (Projects & Issues APIs)

**Focus:** Build out the remaining business logic and RESTful endpoints for the application.

- **Step 1:** Create generic repositories and dedicated services for Projects, Issues, and Time Tracking.
- **Step 2:** Implement `Project` endpoints (Create, Read, Update, Delete) ensuring they are scoped to the user's Team.
- **Step 3:** Implement `Issue (Task)` endpoints (Create issue, Assign to user, Change status: Todo/In-Progress/Done).
- **Step 4:** Add endpoints for Time Tracking (Start/Stop timer for a specific issue).
- **Step 5:** Test all APIs using Postman or Swagger to ensure data is correctly stored and retrieved from PostgreSQL.

---

## Day 4: Frontend Foundation & Authentication UI (Next.js)

**Focus:** Setup the React-based frontend and connect it to the Backend Auth APIs.

- **Step 1:** Initialize the `Next.js (App Router)` project with TypeScript.
- **Step 2:** Configure `Tailwind CSS` with a premium, Linear-inspired design system (Dark mode by default, custom colors, Inter/Roboto fonts).
- **Step 3:** Setup state management (`Zustand`) for storing user/team data and `React Query` for API fetching.
- **Step 4:** Build the UI components for the Login and Signup pages with smooth transitions.
- **Step 5:** Connect the login forms to the .NET Auth API. Implement Next.js middleware to protect private routes and redirect unauthenticated users.

---

## Day 5: Frontend Core UI, Kanban Board & Polish

**Focus:** Build the main application interface, integrate Drag-and-Drop, and polish animations.

- **Step 1:** Build the Application Shell: Sidebar with team projects, top navigation, and responsive layout.
- **Step 2:** Create the general `Dashboard` and Project list views. Integrate them with the Backend API.
- **Step 3:** Implement the `Kanban Board` view using `@dnd-kit` or `react-beautiful-dnd` to allow dragging tasks between statuses. Bind this to the Issue update API.
- **Step 4:** Build the "New Task" modal featuring keyboard shortcuts (e.g., press 'C') and smooth Framer Motion animations.
- **Step 5:** Polish the UI: Add micro-animations (hover effects, glassmorphism on modals), ensure cross-browser compatibility, and verify the entire flow from frontend to database works flawlessly.
