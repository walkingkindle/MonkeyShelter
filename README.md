# 🐒 MonkeyShelter Solution - Setup Guide

This project is a full-stack solution consisting of a **.NET Web API** backend and an **Angular** frontend. Follow the steps below to run it locally.

---

## ✅ Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js (LTS)](https://nodejs.org/en/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or another configured database (e.g., SQLite)

---

## 🚀 How to Run MonkeyShelter Solution

### Step 1 - Build the Project

```bash
dotnet build
```

This will restore and build the .NET backend.

---

### Step 2 - Run the Backend API

From the folder containing the API project (`Presentation`):

```bash
dotnet run
```

The API should be available at:  
```
https://localhost:7008/swagger
```

---

### Step 3 - Install Angular Dependencies

Navigate to the Angular project folder (`Presentation/ClientApp`):

```bash
cd 'Presentation/ClientApp'
npm install
```

---

### Step 4 - Run the Angular Frontend

```bash
ng serve
```

The frontend will run at:
```
http://localhost:4200
```

## 🧪 Running Tests

### Backend Tests

From the test project directory:

```bash
dotnet test
```

---

## 🛠️ Common Troubleshooting

- ❗**CORS Errors**: Ensure CORS is enabled in the backend (in `Program.cs`).
- 🧱 **Migration Errors**: Try running dotnet database migrate if Migration errors occur.
- 🌐 **HTTPS Issues**: Make sure Angular uses the correct protocol (HTTPS or HTTP) to match the backend.

---

## 📂 Project Structure Overview

```
MonkeyShelter/
│
├── Presentation/           # ASP.NET Core Web API
├── Presentation/ClientApp/     # Angular frontend
├── Domain/                      # Domain entities and interfaces
├── Application/                 # Application logic (services, DTOs)
├── Infrastructure/              # EF Core DbContext and migrations
└── README.md
```
