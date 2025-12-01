# 🏡 MagicVilla API — ASP.NET Core 8 RESTful Web API

A clean, scalable, and production-ready RESTful API for managing villa data and villa numbers.
Built using ASP.NET Core 8, Entity Framework Core, and AutoMapper, following best practices in API design, data validation, error handling, and separation of concerns.

---

## 🚀 Features

### **🏠 Villa Management**:

CRUD operations (Create, Read, Update, Delete)

* Validation using DTO models

* Partial updates using JSON Patch

* AutoMapper to simplify model/DTO mapping

### **🔢 Villa Number Management**:

* CRUD operations for villa numbers

* Enforces villa number uniqueness

* Links each villa number to a valid villa

* Full DTO mapping support

### **⚙️ Robust API Architecture**:

* Clean folder structure (Controllers, Models, DTOs, Repository placeholders)

* Consistent response format using APIResponse

* Asynchronous operations for scalability

* Configurable database connection through appsettings.json

### **🗄️ Database Integration**:

* Entity Framework Core with SQL Server

* Code-first migrations

* Data seeding for initial villa records

### **📚 Documentation**:

* Fully integrated Swagger / OpenAPI UI

* Try endpoints interactively

* Highlights all API routes and validation rules


---

## 📂 Project Structure (Clean Architecture)


```

MagicVilla_VillaAPI/
│
├── Controllers/
│   ├── VillaAPIController.cs              # Villa endpoints
│   ├── VillaNumberAPIController.cs        # Villa Number endpoints
│
├── Data/
│   ├── ApplicationDbContext.cs            # EF Core database context
│
├── Models/
│   ├── Villa.cs                           # Villa entity
│   ├── VillaNumber.cs                     # VillaNumber entity
│   │
│   ├── DTO/                               # Data Transfer Objects
│       ├── VillaDTO.cs
│       ├── VillaCreateDTO.cs
│       ├── VillaUpdateDTO.cs
│       ├── VillaNumberDTO.cs
│       ├── VillaNumberCreateDTO.cs
│       ├── VillaNumberUpdateDTO.cs
│
├── Repository/
│   ├── IRepository/                       # Repository interfaces
│   │   ├── IVillaRepository.cs
│   │   ├── IVillaNumberRepository.cs
│   │
│   ├── VillaRepository.cs                 # Villa repository implementation
│   ├── VillaNumberRepository.cs           # VillaNumber repository implementation
│
├── MappingConfig.cs                       # AutoMapper profiles
├── appsettings.json                       # Application configuration
├── Program.cs                             # Application entry point
├── MagicVilla_VillaAPI.csproj             # Project file

```

---

## 🛠️ Tech Stack

| Layer    | Technology                    |
| -------- | ----------------------------- |
| Backend  | ASP.NET Core 8 Web API        |
| ORM      | Entity Framework Core         |
| Database | SQL Server                    |
| Mapping  | AutoMapper                    |
| Docs     | Swagger / OpenAPI             |
| Others   | JSON Patch, LINQ, Async/Await |

---

## 📦 Getting Started

### **✔️ Prerequisites**:

* .NET 8 SDK

* SQL Server (LocalDB or full instance)

* Visual Studio / VS Code

* EF Core CLI tools (optional)

---

## ⚙️ Installation & Setup

### **1️⃣ Clone the Repository**:

```
git clone <repository_url>
cd MagicVilla_VillaAPI
```



### **2️⃣ Update Database Connection**:

* In appsettings.json:

```
"ConnectionStrings": {
    "DefaultSQLConnection": "Data Source=localhost;Initial Catalog=Magic_VillaAPI;Integrated Security=True;Trust Server Certificate=True"
}
```


### **3️⃣ Apply EF Migrations**:

* The project includes all required migrations up to this point, including:

  * 20251108052550_AddVillaTable
  
  * 20251108150430_SeedVillaTable
  
  * 20251108150929_SeedVillaTableWithStaticCreatedDate
  
  * 20251110134438_AddVillaNumberTable
  
  * 20251110211835_AddForeignKeyToVillaTable
  
  * ApplicationDbContextModelSnapshot.cs


* To apply these existing migrations and create the database, run:

```
dotnet ef database update
```

This will:

* Create the Villa table

* Add seeding data

* Create the VillaNumber table

* Add the foreign key relationship

* Apply all schema updates automatically


### **4️⃣ Run the API**:

```
dotnet run
```

### **5️⃣ Open Swagger**:

* Navigate to:

  * https://localhost:port/swagger



---


## 🔥 What We Will Add Next

* This section will grow as YOU upgrade the project.

* Upcoming Enhancements

* Authentication & Authorization (JWT)

* API Versioning

* Repository Pattern abstraction

* Unit of Work

* Caching (Response caching + Redis)

* Global exception handling

* Logging with Serilog

### We will add these step-by-step



---

## 🤝 Contributing

Feel free to fork the project, submit PRs, and propose improvements!


---

## 📬 Contact

📧 Ahmed Samir
ahmedsamir.dev.30@gmail.com

---

## 💙 Acknowledgements

Thanks for checking out MagicVilla API — stay tuned for more upgrades!
