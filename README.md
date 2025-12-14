# TTMS -- Task & Team Management System

## Overview

TTMS is a Task and Team Management System built using ASP.NET Core (.NET
10), Entity Framework Core (Code First), CQRS with MediatR, and
JWT-based authentication.

## Technology Stack

-   ASP.NET Core Web API (.NET 10)
-   Entity Framework Core (Code First)
-   CQRS + MediatR
-   JWT Bearer Authentication
-   SQL Server
-   MSTest
-   Azure DevOps CI/CD (Linux)

## Prerequisites

-   .NET SDK 10.x
-   SQL Server
-   Git
-   dotnet-ef tool

``` bash
dotnet tool install --global dotnet-ef
```

## Clone Repository

``` bash
git clone https://github.com/your-username/TTMS.git
cd TTMS
```

## Configuration

Edit appsettings.json:

``` json
{
  "ConnectionStrings": {
    "TTMSDB": "Server=localhost;Database=TTMSDB;User Id=sa;Password=YourStrongPassword;"
  },
  "Jwt": {
    "Issuer": "TTMS-Issuer",
    "Audience": "TTMS-Audience",
    "Token": "SUPER_SECRET_JWT_KEY_12345"
  }
}
```

## Database Migration

``` bash
dotnet ef migrations add InitialCreate --project src/TTMS.API --startup-project src/TTMS.API
dotnet ef database update --project src/TTMS.API --startup-project src/TTMS.API
```

## Build

``` bash
dotnet restore
dotnet build --configuration Release
```

## Unit Tests

``` bash
dotnet test
```

## Run Application

``` bash
dotnet run --project src/TTMS.API
```

Swagger URL:

    https://localhost:5001/swagger

## Authorization

-   Admin: Manage users and teams
-   Manager: Create and update tasks
-   Employee: View assigned tasks and update status

## CI/CD

-   Configured via azure-pipelines.yml
-   Linux-based build and deployment
-   Runs tests before deployment


