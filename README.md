# 🎓 Student Management System

A full-stack application with **Angular UI** and **ASP.NET Core Web API** using Clean Architecture.

---

## 🏗️ Project Structure

```
Zest (Root Folder)
│
├── UI (Angular Frontend)
│   ├── src
│   ├── app
│   └── angular.json
│
└── API (Backend)
    ├── StudentManagement.sln
    │
    ├── StudentManagement.API            → Presentation Layer (Controllers, Middleware)
    ├── StudentManagement.Application    → Business Logic, DTOs, Interfaces
    ├── StudentManagement.Domain         → Entities
    ├── StudentManagement.Infrastructure → DB, Repositories, JWT
    └── StudentManagement.Tests          → Unit Tests (xUnit)
```

---

## 🚀 Features

* Angular UI (Frontend)
* ASP.NET Core Web API (.NET 9)
* Clean Architecture
* JWT Authentication
* FluentValidation
* Global Exception Handling
* Unit Testing (xUnit + Moq + FluentAssertions)
* Swagger API Docs

---

## 🌐 Frontend (Angular)

### Run UI

```
npm install
ng serve
```

👉 Runs on:

```
http://localhost:4200
```

---

## 🔙 Backend (API)

### Run API

* Press **F5** OR
* `dotnet run`

---

## 🌐 Swagger UI

```
https://localhost:7082/swagger/index.html
```

⚠️ API does NOT auto-redirect to Swagger.

---

## 🔐 Authentication

### Login

```
POST /api/auth/login
```

```json
{
  "username": "admin",
  "password": "admin123"
}
```

---

## 🔑 Default Credentials

| Username | Password |
| -------- | -------- |
| admin    | admin123 |

---

## 🧪 Testing

* xUnit
* Moq
* FluentAssertions

Run via **Test Explorer**

---

## 👨‍💻 Author

Tejpal Rajput
