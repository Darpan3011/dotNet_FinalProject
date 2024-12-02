## Table of Contents
- [Project Overview](#project-overview)
- [Project Structure](#project-structure)
- [Role Management Registration](#role-management-registration)
- [API Usage](#api-usage)
- [Testing Process](#testing-process)

## Project Overview

This solution consists of three projects, each designed with specific responsibilities to ensure a clean architecture.

## Concepts Used
1. Clean Architecture
2. SOLID Principles
3. Dependency Injection
4. Entity Framework
5. Model Validation
6. Authentication using JWT
7. Role-Based Authorization using JWT
8. Filters
9. Global Exception Middleware
10. Logging using Serilog
11. LINQ
12. DbContext
13. Swagger UI for both Development and Production (as it is a web API project)

## Project Structure

### 1. `finalSubmissionDotNet`
Handles incoming HTTP requests and includes:
- **Controllers**: Process requests for both Admin and User roles.
- **Filters**: Custom filters for consistent request preprocessing and postprocessing.
- **Log Folder**: Stores application logs for debugging and monitoring.
- **Exception Middleware**: Global Middleware for all requests made.

### 2. `finalSubmission.Infrastructure`
Manages database interactions and includes:
- **DbContexts**: Contains the database context for Entity Framework.
- **Migrations**: Handles schema changes over time.
- **Repositories**: Implements CRUD operations for database entities.

### 3. `finalSubmission.Core`
Contains core application logic and includes:
- **Models**: Domain models representing application data structures.
- **IdentityModels**: Authentication and authorization models.
- **RepositoryContracts**: Interfaces for repository operations.
- **ServiceContracts**: Interfaces for business logic services.
- **Services**: Implements business logic and orchestrates data access.

## Role Management Registration

### Administrator Role
To register as an administrator:

POST api/Authentication/Register/0
```json 
{
   "username": "admin_username",
   "password": "YourStrongPassword1!"
}
```


Can access APIs in AdminController and AuthenticationController.

### User Role
To register as a User:

POST api/Authentication/Register/1
```json
{
   "username": "user_username",
   "password": "YourStrongPassword1!"
}
```


Can access APIs in UserController and AuthenticationController.

## API Usage

### Login
To login for Admin or User role:

POST api/Authentication/Login/
```json
{
   "username": "admin/user_username",
   "password": "YourStrongPassword1!"
}
```

- If the credentials is correct the token will be generated. Paste this token in the Authorize token as ```Bearer your_token```.
- If you are Admin then you will have access to Admin APIs and if you are User then you will have access to User APIs.

### Task Management
To add a task:
```json
{
   "title": "string",
   "description": "string",
   "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
   "dueDate": "2024-11-30T11:53:26.525Z",
   "status": 0
}
```

## Testing Process

### Steps to Test APIs:
1. Download the project and extract it
2. Go to Package Manager Console
3. Select `finalSubmission.Infrastructure` as project
4. Write command "`Add-Migration Initial`"
5. Write command "`Update-Database`"
6. After successful message, run the project
7. Create a user
8. Create an admin user  
9. List all available users (Copy any one UserId)
10. To Add new task:
   -- Paste the copied UserId
   -- Status is optional (default is Pending)
