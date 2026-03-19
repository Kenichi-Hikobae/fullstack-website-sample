# Fullstack Website Sample
## Project Overview

This project is a full-stack template that demonstrates clean architecture and practical patterns across a modern stack:
> For database setup instructions (JSON import or full dump restore), see the **Database Setup – `db_properties`** section below.

## Tech Stack
- **Backend:** .NET 8 (C#) building a RESTful API with a layered design (**Presentation → Business/Application → Data**).
- **Database:** MongoDB, database **`db_properties`** with two collections: **`owners`** and **`properties`**.
- **Frontend:** React + Next.js with Jest for testing and SPA client navigation.
- **Testing:** xUnit + Moq on the backend; Jest on the frontend.
- **API Client Generation:** **NSwag** runs at build time to generate a typed **TypeScript API client** (`apiClient`) consumed by the frontend.

The goal of the API is to **retrieve properties and owners** using **filtering**, **pagination**, and **DTO mapping** to keep transport contracts clean and stable. This project aims to be a solid starting template that highlights good architecture and design practices in **.NET + React/Next.js**.

## Frontend Pages

- **Home:** Welcome/entry page that links to the app sections.
- **Properties:** Grid view of properties with a filter modal/dialog, server-driven pagination, and incremental data loading.
- **Owners:** Data table listing all registered owners.
- **Property Detail:** Full details for a selected property.

## Why This Project?

This repository is designed as a **reference template**:
- Show **clean separations** between layers and responsibilities.
- Demonstrate **MongoDB integration** with real-world concerns (filters, pagination, DTOs).
- Provide a **generated client** for the frontend to reduce manual boilerplate and mismatches.
- Offer an **end-to-end example** of .NET + Next.js with solid testing practices.

---
# Database Setup – db_properties

This project includes a MongoDB database backup so you can easily replicate the environment on your local machine.  
The database name is **`db_properties`** by default.

You have **two options** to set up the database:
---
## 🔹 Option 1: Restore from BSON Dump (Exact Replica)

This method uses `mongodump`/`mongorestore` to replicate the database, including indexes.  
It is best if you want the database **exactly as it was exported**.

### Restore Steps
1. Extract the backup archive (if provided):
   ```bash
   tar -xzvf db_backup.tar.gz
   ```
   After extraction, you should see a folder like:
   ```
   db_properties/
    ├── users.bson
    ├── users.metadata.json
    ├── properties.bson
    ├── owners.bson
    ...
   ```

2. Restore into MongoDB:
   ```bash
   mongorestore --db db_properties ./db_properties
   ```

3. Verify the import:
   ```bash
   mongosh
   use db_properties
   show collections
   ```

## 🔹 Option 2: Import from JSON (Simple, Human-Readable)

This method uses plain JSON files for each collection.  
It is best for small sample data.

### Import Steps
1. Make sure MongoDB is running locally (`mongod` service).
2. Import each collection using `mongoimport`. For example:

   ```bash
   mongoimport --db db_properties --collection users --file ./database/users.json --jsonArray
   mongoimport --db db_properties --collection properties --file ./database/properties.json --jsonArray
   mongoimport --db db_properties --collection owners --file ./database/owners.json --jsonArray
   ```

   > ⚠️ Use the `--jsonArray` flag if the JSON file contains an array of documents.

3. Verify by connecting to MongoDB:
   ```bash
   mongosh
   use db_properties
   show collections
   ```
## 🔹 Requirements
- [MongoDB Community Server](https://www.mongodb.com/try/download/community) (running locally)  
- [MongoDB Database Tools](https://www.mongodb.com/try/download/database-tools) (for `mongodump`, `mongorestore`, `mongoimport`, `mongoexport`)  
- Alternatively, you can use [MongoDB Compass](https://www.mongodb.com/products/compass) (GUI) to manually import/export JSON files.  

---
# Run application

To be able to run the application you ahve to follow the next steps:

- Setup your mongo Db connection after the databse has been created, in the following path: `\WebsiteServerApp\appsetting.json`
- Run the file `RunAPP.bat` located at the root of the repository.
- Wait until it finishes and you will noticed that the browser will open the application with the correct urls and ports.

You can of course open the correct files to run the application manually.
- API - by running the solution `WebsiteServerApp\WebsiteServerApp.sln` and running it.
- Frontend - Accessing the folder `websiteclientapp` and running the command `npm build` and then `npm startapp` or `npm start`
  
> If something happend during this process, do not hesitate to contact me through email.

# Application Architecture

The application follows a layered architecture designed to promote separation of concerns, scalability, and maintainability:

## Business Layer
This layer encapsulates the core application logic. It contains service classes responsible for implementing business rules, orchestrating workflows, and coordinating interactions between components. It may also include abstractions over database operations to ensure the domain logic remains decoupled from infrastructure concerns.

## Data Access Layer (DAL)
The Data Access Layer manages all interactions with the persistence mechanism. It includes entity models and repository implementations responsible for querying, retrieving, and persisting data. This layer abstracts the underlying database technology, providing a consistent interface to the Business Layer.

## Presentation Layer
This layer exposes the application to external clients. It consists of API controllers that handle HTTP requests and responses, as well as middleware components responsible for cross-cutting concerns such as authentication, authorization, and request validation. It acts as the entry point into the system.

<img width="328" height="824" alt="ArquitectureLayers" src="https://github.com/user-attachments/assets/336aaca3-8b94-48f9-8d12-dd6cdf2a34c8" />

# Website Images

<img width="1898" height="1071" alt="Sample_1" src="https://github.com/user-attachments/assets/a0084c9b-9e15-48a5-967e-7b9e2cbaa028" />

---

<img width="1919" height="1079" alt="Sample_2" src="https://github.com/user-attachments/assets/84c8c433-c864-47fd-ac83-3bb94e0b2cf4" />

---

<img width="1919" height="1079" alt="Sample_3" src="https://github.com/user-attachments/assets/254c3006-f023-4d0e-8fc2-9393e78eef12" />

---

<img width="1919" height="1079" alt="Sample_4" src="https://github.com/user-attachments/assets/ebb5d84f-8d96-4041-9d77-4a117c1500d0" />

---

<img width="1919" height="1079" alt="Sample_5" src="https://github.com/user-attachments/assets/15b003ec-9dbe-47ba-9075-457dba0f151f" />

