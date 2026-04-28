# 🛒 Skinet E-Commerce Platform

Skinet is a modern, full‑stack e‑commerce web application built with **ASP.NET Core 9** and **Angular 21**. The goal of the project is to provide a clean, scalable, and production‑ready codebase for an online store that can be easily extended or used as a learning resource for .NET and Angular developers.

The backend follows a clean **Onion Architecture** with **CQRS**‑like separation (via the **Specification** and **Repository** patterns) and uses **SignalR** for real‑time notifications. The frontend is built with the latest **Angular 21** and uses **TailwindCSS** for a fast, responsive UI.

---

## ✨ Key Features

* **Product Catalogue** – Paginated grid view with real‑time search, filtering (by type, brand), and sorting functionality.
* **Shopping Cart** – Basket management stored server‑side with **Redis** for high performance.
* **User Authentication & Authorisation** – Secure registration/login using **ASP.NET Core Identity** with JWT‑based authentication.
* **Advanced Checkout** – Multi‑step checkout process with order summary, address specification, and payment integration via **Stripe**.
* **Order Management** – Order history, order details view, and real‑time order status updates.
* **Real‑Time Dashboard** – Live notifications for new orders using **SignalR** (admin dashboard integration available).
* **Payment Processing** – Secure credit‑card payments powered by Stripe’s latest API.
* **Error Handling** – Global exception handling, detailed validation errors, and a centralised “buggy” endpoint for testing client‑side error scenarios.

---

## 🧱 Technology Stack

| Layer            | Technology / Library                                                                 |
|------------------|---------------------------------------------------------------------------------------|
| **Backend**      | ASP.NET Core 9 (Web API), C#                                                         |
| **Frontend**     | Angular 21, TypeScript, TailwindCSS, HTML5                                           |
| **Database**     | SQL Server (via Entity Framework Core 9)                                             |
| **Caching**      | Redis (for basket storage and high‑throughput data)                                  |
| **Real‑Time**    | SignalR                                                                              |
| **Payments**     | Stripe.NET SDK                                                                       |
| **Authentication**| ASP.NET Core Identity (JWT)                                                          |
| **Container**    | Docker & Docker Compose                                                              |

---

## 🏗 Architecture Overview

The solution follows **Onion Architecture** with four main projects:

* **`Core`** – Contains domain entities (`Product`, `Order`, `Basket`, etc.), interfaces (repositories, unit‑of‑work), and **Specification** design pattern implementations. This layer has no external dependencies.
* **`Infrastructure`** – Implements data persistence using **Entity Framework Core** and the **Generic Repository** + **Unit‑of‑Work** patterns. It also contains the `StoreContext` and data seeding logic (`StoreContextSeed`). Redis and Stripe services are configured here.
* **`API`** – The ASP.NET Core Web API startup project. Contains RESTful controllers (`ProductsController`, `OrdersController`, `CartController`, `PaymentController`, `AccountController`). This layer sets up dependency injection, JWT‑bearer authentication, Swagger documentation, and SignalR hubs.
* **`client`** – The Angular SPA. Generated with Angular CLI 21, it includes all UI components, services for API communication, a real‑time SignalR client, and TailwindCSS styling.

> **Patterns used** – Generic Repository with Specification pattern, Unit‑of‑Work, dependency injection, DTOs (Data Transfer Objects), and AutoMapper.

---

## 🚀 Getting Started – Run the Project Locally

### Prerequisites

Before you begin, ensure you have the following installed on your machine:

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* [Node.js](https://nodejs.org/) (v20 or later) + npm (v10+)
* [Angular CLI](https://angular.dev/tools/cli) – `npm install -g @angular/cli`
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server LocalDB)
* [Redis](https://redis.io/download) (or use the included `docker-compose.yml` – see below)
* [Git](https://git-scm.com/)

### Step‑by‑Step Setup

 
1. Clone the repository
git clone https://github.com/Mehrad44/skinet.git
cd skinet

2. Configure the Backend (API)
- Restore NuGet packages:
  cd API
  dotnet restore
- Update the database connection string in API/appsettings.json (or use User Secrets).
  Example:
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SkinetDb;Trusted_Connection=True;MultipleActiveResultSets=true",
    "Redis": "localhost"
  }
- Apply Entity Framework migrations and create the database:
  dotnet ef database update --context StoreContext
- (Optional) Seeding is handled automatically by StoreContextSeed.

3. Configure and Run the Frontend (Angular Client)
cd ../client
npm install
ng serve
Frontend runs on https://localhost:4200

4. Run the Backend API
cd ../API
dotnet run
API runs on https://localhost:5001 and http://localhost:5000
Swagger: https://localhost:5001/swagger

5. Configure Stripe (Payment Integration)
- Create Stripe account, get Publishable Key and Secret Key.
- Add to API/appsettings.json:
  "StripeSettings": {
    "PublishableKey": "your_stripe_publishable_key",
    "SecretKey": "your_stripe_secret_key"
  }

6. Redis Cache Setup
- Use Docker: docker-compose up -d redis
- Or install Redis locally (default endpoint localhost:6379)

7. Running with Docker Compose (SQL Server + Redis)
docker-compose up -d

8. Folder Structure
skinet/
├── API/
├── Core/
├── Infrastructure/
├── client/
└── docker-compose.yml

9. Testing
- Backend: No tests included (you can add xUnit/NUnit)
- Frontend: cd client && ng test

10. License
Educational purposes only. No explicit license. Add MIT if needed.
