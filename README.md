# Clean Architecture Essencial

🇧🇷 [Leia em Português](LEIAME.md)

A study and reference project demonstrating the implementation of **Clean Architecture** with **.NET 10**, combining modern patterns such as **CQRS**, **Repository Pattern**, and **MediatR** in a real-world product and category management application.

## Architecture

The project follows the principles of **Clean Architecture**, where dependencies always point inward, keeping the domain isolated from frameworks, databases, and external interfaces.

```
┌──────────────────────────────────────────────────────┐
│                  Presentation Layer                  │
│              WebUI (MVC)  |  WebAPI (REST)           │
├──────────────────────────────────────────────────────┤
│               Infrastructure Layer                   │
│         Infra.Data (EF Core)  |  Infra.IoC           │
├──────────────────────────────────────────────────────┤
│               Application Layer                      │
│     Services | DTOs | Commands | Queries | Handlers  │
├──────────────────────────────────────────────────────┤
│                  Domain Layer                        │
│          Entities | Interfaces | Validations         │
└──────────────────────────────────────────────────────┘
```

## 🗂️ Project Structure

| Project | Responsibility |
|---|---|
| `Domain` | Entities, repository interfaces, domain validations, and authentication contracts |
| `Application` | Use cases via Services and CQRS (Commands, Queries, Handlers), DTOs, and AutoMapper profiles |
| `Infra.Data` | EF Core implementation with PostgreSQL, repositories, Identity, and Migrations |
| `Infra.IoC` | Dependency registration for WebUI and WebAPI |
| `WebUI` | Web interface with ASP.NET Core MVC and Razor Views |
| `WebAPI` | REST API with ASP.NET Core Web API and OpenAPI |
| `Tests` | Unit tests for domain entities |

## Technologies

- [.NET 10](https://dotnet.microsoft.com/)
- [ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core/mvc/) — web interface
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/) — REST API
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) with [Npgsql](https://www.npgsql.org/) — ORM for PostgreSQL
- [MediatR](https://github.com/jbogard/MediatR) — CQRS pattern implementation
- [AutoMapper](https://automapper.org/) — mapping between entities, DTOs, and Commands
- [ASP.NET Core Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity) — authentication and authorization with Roles
- [JWT Bearer](https://learn.microsoft.com/aspnet/core/security/authentication/jwt-authn) — token-based authentication for the REST API
- [Scalar](https://scalar.com/) — interactive OpenAPI documentation UI
- [xUnit](https://xunit.net/) — unit testing

## Design Patterns

- **Clean Architecture** — layered separation with inverted dependencies
- **CQRS** (Command Query Responsibility Segregation) — read and write separation via MediatR
- **Repository Pattern** — abstraction of data access in the domain
- **Dependency Injection** — inversion of control native to ASP.NET Core

## Domain

### Entities

**`Product`**
- `Name`, `Description`, `Price`, `Stock`, `Image`
- Relationship with `Category`
- Domain validations encapsulated within the entity itself

**`Category`**
- `Name`
- Collection of associated `Products`
- Domain validations encapsulated within the entity itself

## Setup and Running

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/)

### Environment Variables

The database connection string and JWT settings are read from environment variables:

```bash
# Windows (PowerShell)
$env:DEV_DB      = "Host=localhost;Database=CleanArchDb;Username=postgres;Password=your_password"
$env:JWT_KEY     = "your_super_secret_key_at_least_32_chars"
$env:JWT_ISSUER  = "your_issuer"
$env:JWT_AUDIENCE = "your_audience"

# Linux/macOS
export DEV_DB="Host=localhost;Database=CleanArchDb;Username=postgres;Password=your_password"
export JWT_KEY="your_super_secret_key_at_least_32_chars"
export JWT_ISSUER="your_issuer"
export JWT_AUDIENCE="your_audience"
```

### Migrations

Apply the migrations to create the database:

```bash
dotnet ef database update --project Infra.Data --startup-project WebUI
```

### Running the Application

**Web Interface (WebUI):**
```bash
dotnet run --project WebUI
```

**REST API (WebAPI):**
```bash
dotnet run --project WebAPI
```

The OpenAPI documentation will be available at `https://localhost:{port}/scalar/v1` in the development environment.

### Running the Tests

```bash
dotnet test
```

## Authentication

### WebUI

The web interface uses **ASP.NET Core Identity** with cookie-based authentication and an initial seed of users and roles.

- When the `WebUI` starts, default roles and users are automatically created via `SeedUserRoleInitial`.
- Denied access redirects to `/Account/Login`.

### WebAPI — JWT

The REST API is protected with **JWT Bearer** tokens. The token configuration is provided via environment variables (`JWT_KEY`, `JWT_ISSUER`, `JWT_AUDIENCE`).

**Token endpoints** (`/api/Token`):

| Method | Endpoint | Auth required | Description |
|--------|----------|:---:|-------------|
| `POST` | `/api/Token/LoginUser` | No | Authenticate and receive a JWT token |
| `POST` | `/api/Token/CreateUser` | Yes | Register a new API user (admin only) |

Tokens expire after **10 minutes**. Include the token in the `Authorization` header of subsequent requests:

```
Authorization: Bearer <token>
```
