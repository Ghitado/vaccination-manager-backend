# VaccinationManager

## Vaccination Manager Backend

This repository contains the backend for the technical challenge "Vaccination Manager" — an API built with .NET 8, Entity Framework Core and a layered architecture (API ? Application (UseCases) ? Domain ? Infrastructure).

This README documents setup, architecture, API routes, usage examples and architectural decisions in a clear and concise way.

## Table of contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Repository layout](#repository-layout)
- [Run locally](#run-locally)
  - [Database](#database)
- [Configuration](#configuration)
- [API documentation (Swagger)](#api-documentation-swagger)
- [API routes and examples](#api-routes-and-examples)
  - [Persons](#persons)
  - [Vaccines](#vaccines)
  - [VaccinationRecords](#vaccinationrecords)
- [Validation and error handling](#validation-and-error-handling)
- [Architecture and decisions](#architecture-and-decisions)
- [Testing](#testing)
- [Submission checklist](#submission-checklist)
- [Next steps / optional improvements](#next-steps--optional-improvements)
- [Author](#author)

## Overview

The application manages persons, vaccines and vaccination records. It was developed prioritizing clarity, testability and common REST conventions appropriate for a technical challenge.

Key technologies:
- .NET 8
- Entity Framework Core
- Mapster for DTO mapping
- Swashbuckle / Swagger for API documentation

## Prerequisites

- .NET 8 SDK
- Git
- An IDE: Visual Studio 2022, VS Code or CLI
- A supported relational database (SQLite is sufficient for local development)

## Repository layout

- `src/VaccinationManager.Api/` — Web API (Program.cs, controllers)
- `src/VaccinationManager.Application/` — application layer (use cases, DTOs, DI)
- `src/VaccinationManager.Domain/` — domain entities and domain exceptions
- `src/VaccinationManager.Infrastructure/` — EF Core DbContext and repositories
- `tests/` — unit tests (domain tests present)

## Run locally

1. Clone the repository:

git clone https://github.com/Ghitado/vaccination-manager-backend.git 
cd vaccination-manager-backend

2. Restore and build:

dotnet restore
dotnet build

3. Apply EF Core migrations:

dotnet ef database update --project src/VaccinationManager.Infrastructure --startup-project src/VaccinationManager.Api

If you prefer SQLite in development, update the connection string to point to a local file. For SQL Server or PostgreSQL, update the connection string accordingly.

4. Run the API:

cd src/VaccinationManager.Api
dotnet run

Open the URL shown in the console (for example `https://localhost:5001`) and browse `/swagger` to see the API documentation.

### Database

- The DbContext is `VaccinationManagerDbContext` in `src/VaccinationManager.Infrastructure`.
- Use EF Core migrations to manage schema changes.

## Configuration

- Main configuration is read from `appsettings.json` and environment variables.
- Primary configuration key:
  - `ConnectionStrings:Default` — database connection string.
- XML documentation for Swagger is enabled via the project configuration. In Visual Studio check __Project Properties > Build__ and ensure the __GenerateDocumentationFile__ option is enabled.

## API documentation (Swagger)

- Swagger is configured in `Program.cs` and includes XML comments for public controllers and DTOs.
- Swagger UI is available in the Development environment and shows endpoint summaries, models and response types.

## API routes and examples

Base URL: `https://localhost:5001`

All endpoints consume and produce JSON.

Routes use `api/[controller]` naming (for example `/api/person`, `/api/vaccine`, `/api/vaccinationrecord`).

### Persons

- Create person
  - POST `/api/person`
  - Body:

{
  "name": "João Silva"
}

- Response (201 Created)
  - Body (example):

{
  "id": "8a1f...",
  "name": "João Silva"
}

- List (pagination)
  - GET `/api/person?pageNumber=1&pageSize=10`
  - Response: `200 OK` with `PaginatedResult<PersonResponse>`

- Get by id
  - GET `/api/person/{id}`
  - Response: `200 OK` or `404 Not Found`

### Vaccines

- Create vaccine
  - POST `/api/vaccine`
  - Body:

{
  "name": "VacinaX"
}

- Get by id
  - GET `/api/vaccine/{id}` — `200 OK` or `404`

- List (pagination)
  - GET `/api/vaccine?pageNumber=1&pageSize=10` — `200 OK`

### VaccinationRecords

- Create vaccination record
  - POST `/api/vaccinationrecord`
  - Body:

{
  "personId": "<person-guid>",
  "vaccineId": "<vaccine-guid>",
  "date": "2025-11-19T00:00:00Z",
  "dose": 1
}

- Response (201 Created)
  - Location header points to `GET /api/vaccinationrecord/{id}`
  - Response body: `VaccinationRecordResponse` including `id`

- Get by id
  - GET `/api/vaccinationrecord/{id}` — `200 OK` or `404 Not Found`

- List (paginated)
  - GET `/api/vaccinationrecord?pageNumber=1&pageSize=10` — `200 OK`

- List by person
  - GET `/api/vaccinationrecord/person/{personId}` — `200 OK` (returns person's vaccination records)

- Delete
  - DELETE `/api/vaccinationrecord/{id}` — `204 No Content` or `404 Not Found`

## Validation and error handling

Current approach:

- Contract validation (model validation): controllers use `[ApiController]` which enforces model validation when DTOs include DataAnnotations. Consider adding FluentValidation for richer, centralized DTO rules.
- Domain validation: business rules are implemented in domain entities and throw `DomainException` on violation.
- Error mapping: `DomainException` indicates business validation errors and should map to `400 Bad Request` with a `ProblemDetails` payload. Not found cases map to `404 Not Found`. Unhandled exceptions map to `500 Internal Server Error` with `ProblemDetails`.
- A global exception handling middleware is recommended to map exceptions consistently to HTTP responses.

## Architecture and decisions

- Layered architecture:
  - Controllers: HTTP entry point, input normalization and basic validation.
  - Application / UseCases: business orchestration and application rules.
  - Domain: entities and business rules (validation inside constructors/methods).
  - Infrastructure: persistence using EF Core and repository implementations.

- Mapping: Mapster provides lightweight DTO ? entity mapping with minimal configuration.

- Pagination: repositories return `PaginatedResult<T>` containing `Items`, `TotalCount`, `PageNumber` and `PageSize`.

- Pragmatic design: for a time-limited challenge, priority was clarity and testability rather than introducing many abstractions. Additional generic layers are avoided unless they clearly reduce duplication.

## Testing

- Domain unit tests are available in `tests/`. Run them with:

dotnet test

- Recommended submission tests:
  - One or two integration tests using `WebApplicationFactory` covering Create ? GetById ? Delete flows.

## Submission checklist

- Code builds with `dotnet build` 
- Endpoints documented via Swagger
- Create endpoints return `201 Created` with Location header and body 
- Pagination implemented 
- Centralized error handling (middleware) is suggested and partially applied — include it if time allows

## Next steps / optional improvements

- Add FluentValidation for DTO validation with consistent error messages.
- Add integration tests for main endpoints.
- Add authentication/authorization if required and document it in Swagger.
- Add request/response examples in Swagger using `Swashbuckle.AspNetCore.Filters`.

## Author

By Thiago de Melo Mota.
Implemented as part of the technical challenge submission.
