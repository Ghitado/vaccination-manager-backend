# VaccinationManager

## Vaccination Manager Backend

This repository contains the backend for the technical challenge "Vaccination Manager" — an API implemented with .NET 8, Entity Framework Core, and a layered architecture (API ? Application ? Domain ? Infrastructure).

The purpose of this README is to document the entire system: setup, architecture, API routes with examples, validation and error handling, testing and deployment notes, and the main design decisions made during implementation.

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
- [Deployment notes](#deployment-notes)
- [Contribution and style notes](#contribution-and-style-notes)
- [Author](#author)

## Overview

The application manages people, vaccines, and vaccination records. It was implemented to be clear, testable, and suitable for a technical challenge submission. The code favors readability and explicit behavior over premature abstraction.

Key technologies:
- .NET 8
- Entity Framework Core (EF Core)
- Mapster for DTO mappings
- Swashbuckle / Swagger for API documentation

## Prerequisites

- .NET 8 SDK
- Git
- An IDE: Visual Studio 2022, VS Code, or the .NET CLI
- A relational database. SQLite is sufficient for local development.

## Repository layout

- `src/VaccinationManager.Api/` — Web API project (Program.cs, controllers)
- `src/VaccinationManager.Application/` — application layer (use cases, DTOs, dependency injection)
- `src/VaccinationManager.Domain/` — domain entities and domain exceptions
- `src/VaccinationManager.Infrastructure/` — persistence (DbContext, EF Core repository implementations)
- `tests/` — unit tests (domain tests present)

## Run locally

1. Clone the repository:

    ```bash
    git clone https://github.com/Ghitado/vaccination-manager-backend.git
    cd vaccination-manager-backend
    ```

2. Restore packages and build:

    ```bash
    dotnet restore
    dotnet build
    ```

3. Apply EF Core migrations (example):

    ```bash
    dotnet ef database update --project src/VaccinationManager.Infrastructure --startup-project src/VaccinationManager.Api
    ```

4. Run the API:

    ```bash
    cd src/VaccinationManager.Api
    dotnet run
    ```

Open the address shown in the console (for example `https://localhost:5001`) and browse `/swagger` to view API documentation.

### Database

- The main DbContext is `VaccinationManagerDbContext` in `src/VaccinationManager.Infrastructure`.
- By default, the project is configured for local development. To change providers (SQL Server, PostgreSQL), update the connection string in `appsettings.json` or environment variables and install the corresponding EF Core provider package.

## Configuration

Configuration is read from `appsettings.json` and environment variables. Important keys:

- `ConnectionStrings:Default` — main database connection string

XML documentation for Swagger is enabled via the API project configuration. In Visual Studio, this corresponds to __Project Properties > Build > XML documentation file__ (GenerateDocumentationFile).

## API documentation (Swagger)

- Swagger/OpenAPI is configured and exposes the API in Development at `/swagger`.
- Controllers include XML comments and `ProducesResponseType` attributes so each endpoint shows summaries and response types in the UI.
- The project includes Swashbuckle configuration in `Program.cs` to include XML comments and to enable operation annotations.

## API routes and examples

Base URL: `https://localhost:5001`

All endpoints accept and return JSON. Routes use `api/[controller]`.

Return models are DTOs under `src/VaccinationManager.Application/Dtos`. The API never returns EF tracked entities directly.

### Persons

- Create person
  - POST `/api/person`
  - Request body:

    ```json
    {
      "name": "John Doe"
    }
    ```

  - Response: `201 Created` with Location header pointing to `GET /api/person/{id}` and the created resource in the response body.

- List persons (paginated)
  - GET `/api/person?pageNumber=1&pageSize=10`
  - Response: `200 OK` with a `PaginatedResult<PersonResponse>`:

    ```json
    {
      "items": [ { "id": "...", "name": "John Doe" } ],
      "totalCount": 1,
      "pageNumber": 1,
      "pageSize": 10
    }
    ```

- Get person by id
  - GET `/api/person/{id}`
  - Response: `200 OK` with `PersonResponse`, or `404 Not Found` if not present.

### Vaccines

- Create vaccine
  - POST `/api/vaccine`
  - Request body:

    ```json
    {
      "name": "VaccineX"
    }
    ```

  - Response: `201 Created` with created resource.

- Get vaccine by id
  - GET `/api/vaccine/{id}` — `200 OK` or `404 Not Found`.

- List vaccines (paginated)
  - GET `/api/vaccine?pageNumber=1&pageSize=10` — `200 OK` with `PaginatedResult<VaccineResponse>`.

### VaccinationRecords

- Create vaccination record
  - POST `/api/vaccinationrecord`
  - Request body:

    ```json
    {
      "personId": "<person-guid>",
      "vaccineId": "<vaccine-guid>",
      "date": "2025-11-19T00:00:00Z",
      "dose": 1
    }
    ```

  - Response: `201 Created`. Location header points to `GET /api/vaccinationrecord/{id}`. Response body contains `VaccinationRecordResponse` including `id` and related nested DTOs when available.

- Get vaccination record by id
  - GET `/api/vaccinationrecord/{id}` — `200 OK` or `404 Not Found`.

- List vaccination records (paginated)
  - GET `/api/vaccinationrecord?pageNumber=1&pageSize=10` — `200 OK` with `PaginatedResult<VaccinationRecordResponse>`.

- List by person
  - GET `/api/vaccinationrecord/person/{personId}` — `200 OK` returning the vaccination records for a person.

- Delete
  - DELETE `/api/vaccinationrecord/{id}` — `204 No Content` on success or `404 Not Found` if the record does not exist.

## Validation and error handling

Validation strategy used in this project:

- Model/contract validation: controllers use `[ApiController]` which enforces model validation for incoming DTOs. DataAnnotations or FluentValidation can be used to provide validation rules and automatic 400 responses.
- Domain validation: domain entities and domain operations throw `DomainException` to indicate business-rule violations. Use cases and repositories should propagate or translate these exceptions as appropriate.
- Error mapping: a global exception handling middleware is recommended to map exceptions to proper HTTP responses. Suggested mapping:
  - `DomainException` => 400 Bad Request (or 422 Unprocessable Entity if preferred)
  - `ArgumentException` / `ArgumentNullException` => 400 Bad Request
  - `KeyNotFoundException` or a NotFound-specific exception => 404 Not Found
  - any other unhandled exception => 500 Internal Server Error

Responses for errors should follow Problem Details (`application/problem+json`) where applicable to provide consistent error structure.

## Architecture and decisions

Rationale for the most important design choices:

- Layered architecture: controllers handle HTTP concerns and input normalization; use cases (application) contain orchestration and application-level rules; domain contains core entities and invariants; infrastructure contains EF Core and repository implementations.

- Mapping: Mapster is used for mapping between domain models and DTOs. Mapping is done in use cases or at the boundary to avoid leaking EF entities through the API.

- Persistence: EF Core is used with repositories to encapsulate queries and pagination. Repositories return `PaginatedResult<T>` to keep pagination concerns consistent.

- Pagination: Implemented with `Skip`/`Take` and a total count. Controllers accept `pageNumber` and `pageSize` query parameters. Default values are applied when parameters are not provided.

- Exceptions: Domain validation uses `DomainException`. Use `ArgumentException` for API/input contract violations where appropriate. Centralize exception-to-HTTP mapping in middleware for consistent behavior.

- Simplicity: For the purposes of the challenge, the code favors clarity and directness over extra abstraction. Generic repositories or heavy frameworks were avoided because they would increase complexity without clear benefit for this scope.

## Testing

- Unit tests for domain entities are under `tests/` and can be executed with:

    ```bash
    dotnet test
    ```

- Recommended tests to include before submission:
  - Integration tests using `WebApplicationFactory` that cover Create ? GetById ? Delete flows for one resource.
  - Tests that assert validation behavior and error mapping.

## Deployment notes

- The application can be deployed to any hosting environment that supports .NET 8 (Azure App Service, containers, etc.).
- Configure production database connection via environment variables and ensure migrations are applied during deployment.
- For production, enable structured logging and set up health checks and monitoring.

## Contribution and style notes

- Follow the existing code style and naming conventions present in the repository.
- Use semantic commit messages (type(scope): description) for changes. Examples used in this project: `feat(domain/entities): create Person entity`, `test(domain/tests/entities): add PersonTests`.
- Keep controllers thin: validation and normalization in controllers, business rules in use cases/domain.

## Author

By Thiago de Melo Mota. Implementation prepared as part of a technical challenge submission. 
