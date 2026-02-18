Mini Order Management Application

This project is a Clean Architecture demonstration built with .NET 8. It showcases Domain-Driven Design (DDD), CQRS, Repository and Unit of Work patterns, and a structured enterprise-ready approach using Entity Framework Core and SQL Server.

This README explains the project structure, architectural decisions, Repository and Unit of Work usage, CQRS implementation, database migrations, and how to access Swagger.

Overview

The application is designed using Clean Architecture principles to ensure clear separation of concerns and long-term maintainability. The core business logic is isolated from infrastructure concerns, making the system testable and extensible.

The design implementation:

Clean separation between domain, application, infrastructure, and presentation layers
Encapsulation of business rules inside domain entities
Explicit separation of read and write operations using CQRS
Abstracted data access using Repository pattern
Transaction management using Unit of Work

Project Structure

The solution follows a layered architecture aligned with Clean Architecture principles.

Domain Layer
Contains core business entities such as Customer, Order, and CustomerProfile.
Includes business rules, aggregates, and value objects.
Has no dependency on external frameworks.

Application Layer
Contains use cases, command handlers, query handlers, interfaces, and validation logic.
Implements CQRS by separating commands and queries.
Depends only on the Domain layer.

Infrastructure Layer
Implements data access using Entity Framework Core.
Contains DbContext, repository implementations, and Unit of Work implementation.
Handles database configuration and persistence concerns.

Presentation Layer
Provides RESTful endpoints through ASP.NET Core controllers.
Acts as the entry point to the application.
Integrates Swagger for API documentation.

Architecture Approach

The project follows Domain-Driven Design.

The Domain layer is the core of the system. Business logic is encapsulated inside entities instead of being spread across services. The Customer entity acts as the aggregate root and manages related entities such as Order and CustomerProfile.

The architecture enforces dependency flow inward. Outer layers depend on inner layers, but the Domain layer does not depend on any other layer. This ensures independence of business logic from infrastructure and frameworks.

Repository and Unit of Work

Repository Pattern

The Repository pattern abstracts data access logic and provides a collection-like interface for working with domain entities.

Instead of directly using DbContext in the Application layer, repositories are defined as interfaces in the Application layer and implemented in the Infrastructure layer. This keeps the Application layer independent from Entity Framework.

Repositories handle operations such as:

Adding new entities
Fetching entities by identifier
Retrieving collections
Updating and removing entities

Unit of Work Pattern

The Unit of Work coordinates multiple repositories and manages transaction boundaries.

It ensures that multiple operations are committed as a single transaction. The Application layer interacts with the Unit of Work interface, and the Infrastructure layer provides its implementation using Entity Framework Core.

This guarantees consistency and ensures that either all changes succeed or none are persisted.

CQRS Implementation

The project implements Command Query Responsibility Segregation.

Commands

Commands are responsible for modifying application state. Examples include creating a customer or placing an order.

Each command has:

A command model representing input data
A validator using FluentValidation
A command handler that contains the business logic

Command handlers use repositories and Unit of Work to persist changes.

Queries

Queries are responsible for retrieving data without modifying the system state.

Each query has:

A query model
A query handler

Query handlers fetch data through repositories and return DTOs. Queries do not trigger domain changes.

This separation improves clarity, testability, and scalability. Commands and queries can evolve independently and can be optimized separately if needed.

Database Migrations

The project uses Entity Framework Core with a Code First approach.

To create a new migration:

dotnet ef migrations add MigrationName --project MiniOrderManagement.Infrastructure --startup-project MiniOrderManagement.API

To apply migrations to the database:

dotnet ef database update --project MiniOrderManagement.Infrastructure --startup-project MiniOrderManagement.API

Migrations are stored inside the Infrastructure layer under the Migrations folder.

Before running migrations, ensure that the connection string in appsettings.json is correctly configured to point to your SQL Server instance.

Accessing Swagger

Swagger is enabled automatically in Development mode.

To access Swagger:

Run the application using:

dotnet run

Open a browser and navigate to:

https://localhost:7202/swagger/index.html

Swagger provides:

Interactive API documentation
Request and response schema visualization
Ability to test endpoints directly from the browser

This allows you to explore all available endpoints and execute API calls without using external tools.

This project demonstrates how to structure a maintainable and scalable .NET application using Clean Architecture, DDD, CQRS, Repository, and Unit of Work patterns in a practical implementation.