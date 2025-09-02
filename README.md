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
> You can also find the backup in the path 'MongoDB Backup\dump'
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


