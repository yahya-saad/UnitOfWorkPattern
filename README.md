# UnitOfWorkAPI

This repository contains a .NET Core Web API project demonstrating the Unit of Work (UoW) pattern with two versions:

1. Using synchronous methods.
2. Using asynchronous methods.

## Project Structure

```
├── UnitOfWork.Core
│ ├── Interfaces
│ │ ├── IBaseRepository.cs
| | ├── IBooksRepository.cs
│ │ └── IUnitOfWork.cs
│ └── Models
│   ├── Author.cs
│   └── Book.cs
│
├── UnitOfWork.EF
│ ├── Repositories
│ │ ├── BaseRepository.cs
│ │ └── BooksRepository.cs
| ├── AppDbContext.cs
│ └── UnitOfWork.cs
|
│
├── UnitOfWork.API
│ ├── Controllers
│ │ ├── v1 (sync)
│ │ │ ├── AuthorsController.cs
│ │ │ └── BooksController.cs
│ │ └── v2 (async)
│ │ ├── AuthorsController.cs
│ │ └── BooksController.cs
│ ├── Program.cs
| ├── GlobalUsings.cs
│ └── appsettings.json
```
