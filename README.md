# GoodreadsBooksResolver 📚

A .NET console/worker application that processes locally stored books (ebooks files) data and enriches it using external book metadata sources such as **Open Library**.

This project is designed to read book inputs, resolve book details locally or via APIs, and persist structured book information using Entity Framework Core. The results from localDB can be used to populate Goodreads bulk upload CSV templates for a quick and easy update of your "Read" book list.

---

## ✨ Features

- 📥 Import and process Ebooks file data
- 🔎 Resolve book information via **Open Library**
- 🧠 Local book lookup to reduce duplicate API calls
- 🗄️ Persistence using **Entity Framework Core**
- ⚙️ Modular architecture with repositories, services, and workers
- 📊 Console-based progress reporting
- 🧪 Migration-ready database setup

---

## 🏗️ Project Structure

```
GoodreadsBooks/
├── GoodreadsBooksResolver.sln
├── GoodreadsBooks/
│   ├── goodreads/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── Models/
│   │   ├── Repository/
│   │   ├── Resources/
│   │   ├── Services/
│   │   ├── Workers/
│   │   └── Utility/
│   └── Migrations/
└── README.md
```

### Key Layers

- **Models** – Domain and database entities  
- **Repository** – Data access layer (EF Core + abstractions)  
- **Resources** – External API repositories (e.g. Open Library)  
- **Services** – Business logic  
- **Workers** – Background/processing workflows  
- **Utility** – Helper utilities (e.g. progress bar)

---

## 🛠️ Tech Stack

- **.NET / C#**
- **Entity Framework Core**
- **Microsoft.Extensions.Hosting**
- **Dependency Injection**
- **Open Library API**
- **JSON-based configuration**

---

## ⚙️ Configuration

Update `appsettings.json` with:

- Database connection string
- Goodreads input file paths (if applicable)
- Open Library or external API settings

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=GoodreadsBooks;Trusted_Connection=True;"
  }
}
```

---

## 🚀 Getting Started

### Prerequisites

- .NET SDK (6.0 or later recommended)
- SQL Server (or configured database provider)

### Run the application

```bash
dotnet restore
dotnet build
dotnet run --project GoodreadsBooks
```

### Database Setup

Apply migrations:

```bash
dotnet ef database update
```

---

## 🧠 How It Works

1. Application starts using `IHostBuilder`
2. Configuration and services are registered via dependency injection
3. Workers orchestrate:
   - Reading input data
   - Resolving book metadata locally or via Open Library
   - Persisting results to the database
4. Progress is displayed in the console

---

## 📌 Use Cases

- Cleaning and enriching Goodreads exports
- Building a personal or analytical book database
- Learning layered architecture in .NET
- Practicing API integration + EF Core

---

## 🔮 Future Improvements

- Add support for additional book APIs
- Improve ISBN matching and deduplication
- Export enriched data to CSV/JSON
- Add unit tests
- Add logging dashboards

---

## 🤝 Contributing

Contributions are welcome!

1. Fork the repo
2. Create a feature branch
3. Commit changes
4. Open a pull request

---

## 📄 License

This project is open source. Add a license file if you plan to distribute or reuse it publicly.

---

## 👤 Author

**Anamika Mondal**  
GitHub: https://github.com/mana356
