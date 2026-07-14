# Smart Task Management System

## Project Overview

Smart Task Management System is a full-stack web application designed to help users efficiently manage, organize, and track their daily tasks.

The system provides a centralized platform where users can create tasks, update task information, monitor task progress, and maintain better productivity through structured task management.

The application follows a modern client-server architecture with a separate frontend and backend implementation. The backend provides RESTful APIs for data management, while the frontend provides an interactive and responsive user interface.

The main goal of this project is to demonstrate a scalable task management solution using modern web technologies and software development best practices.

---

# Key Features

- Create new tasks
- View task lists
- Update existing tasks
- Delete tasks
- Manage task status
- Track task progress
- Responsive user interface
- REST API based communication
- Separation of frontend and backend responsibilities
- Clean and maintainable project structure

---

# Application Architecture

The project follows a three-layer architecture:

```
User Interface Layer
        |
        |
Angular Frontend
        |
        |
REST API Communication
        |
        |
.NET Core Web API
        |
        |
Database Layer
```

### Frontend

Responsible for:

- User interaction
- Data presentation
- Form handling
- API communication
- Client-side validation


### Backend

Responsible for:

- Business logic
- Data processing
- API endpoints
- Database communication
- Server-side validation


### Database

Responsible for:

- Data persistence
- Task information storage
- Application data management

---

# Technology Stack

## Backend Technologies

| Technology | Purpose |
|------------|---------|
| .NET Core Web API | Backend API development |
| C# | Programming language |
| Entity Framework Core | ORM for database communication |
| LINQ | Data querying |
| SQL Server | Database |
| REST API | Client-server communication |
| Dependency Injection | Application architecture |

---

## Frontend Technologies

| Technology | Purpose |
|------------|---------|
| Angular | Frontend framework |
| TypeScript | Programming language |
| HTML5 | Page structure |
| CSS3 | Styling |
| Bootstrap | Responsive UI design |
| RxJS | Reactive programming |

---

## Development Tools

| Tool | Purpose |
|------|---------|
| Visual Studio | Backend development |
| Visual Studio Code | Frontend development |
| Git | Version control |
| GitHub | Repository hosting |
| Postman | API testing |

---

# Project Setup Instructions

## Prerequisites

Install the following tools before running the application:

- .NET SDK
- Node.js
- npm
- Angular CLI
- SQL Server
- Git


Verify installations:

### Check .NET

```bash
dotnet --version
```

### Check Node.js

```bash
node --version
```

### Check Angular CLI

```bash
ng version
```

---

# Backend Setup

## Step 1: Clone Repository

```bash
git clone https://github.com/Mahbub20/Smart-Task-Management-System.git
```

Navigate to backend:

```bash
cd Backend
```

---

## Step 2: Configure Database

Open:

```
appsettings.json
```

Update the database connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server Connection String"
  }
}
```

---

## Step 3: Restore Dependencies

Run:

```bash
dotnet restore
```

---

## Step 4: Apply Database Migration

Run:

```bash
dotnet ef database update
```

---

## Step 5: Run Backend Application

Execute:

```bash
dotnet run
```

The API will start on:

```
https://localhost:<port>
```

---

# Frontend Setup

Navigate to frontend:

```bash
cd Frontend
```

---

## Install Packages

Run:

```bash
npm install
```

---

## Run Angular Application

Execute:

```bash
ng serve
```

The application will be available at:

```
http://localhost:4200
```

---

# API Overview

The backend exposes RESTful APIs for task management operations.

---

# Task APIs

## Get All Tasks

### Endpoint

```
GET /api/tasks
```

### Description

Returns all available tasks.

---

## Get Task By Id

### Endpoint

```
GET /api/tasks/{id}
```

### Description

Returns details of a specific task.

---

## Create Task

### Endpoint

```
POST /api/tasks
```

### Request Example

```json
{
  "title": "Complete documentation",
  "description": "Prepare project documentation",
  "status": "Pending"
}
```

### Response

Returns the newly created task information.

---

## Update Task

### Endpoint

```
PUT /api/tasks/{id}
```

### Description

Updates an existing task.

---

## Delete Task

### Endpoint

```
DELETE /api/tasks/{id}
```

### Description

Deletes a task from the system.

---

# Folder Structure

```
Smart-Task-Management-System
│
├── Backend
│   │
│   ├── Controllers
│   │   ├── TaskController.cs
│   │   └── API Controllers
│   │
│   ├── Models
│   │   └── Entity Models
│   │
│   ├── DTOs
│   │   └── Data Transfer Objects
│   │
│   ├── Services
│   │   └── Business Logic
│   │
│   ├── Data
│   │   └── Database Context
│   │
│   └── Program.cs
│
│
├── Frontend
│
│   ├── src
│   │
│   ├── app
│   │   │
│   │   ├── components
│   │   │
│   │   ├── services
│   │   │
│   │   ├── models
│   │   │
│   │   └── shared
│   │
│   ├── angular.json
│   └── package.json
│
│
├── README.md
│
└── PROMPTS.md
```

---

# Future Enhancements

Possible improvements:

- User authentication and authorization
- Role-based access control
- Task priority management
- Task reminders
- Notifications
- Dashboard analytics
- AI-based task recommendations
- Mobile application support


---

# License

This project is developed for learning, demonstration, and portfolio purposes.
