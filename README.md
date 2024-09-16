# ProductCatalogService

## Overview

ProductCatalogService is a microservice for managing product data in an e-commerce system. It provides a set of APIs for performing CRUD operations on products and stores product data in a PostgreSQL database.

## Features

- CRUD operations for products (Create, Read, Update, Delete)
- Integration with PostgreSQL database
- Serilog logging to text files (configurable)
- Error handling middleware for centralized error logging
- Secure and validated input to prevent security vulnerabilities

## Technologies

- .NET 8.0
- PostgreSQL
- Entity Framework Core
- Serilog for logging
- Docker for containerization

## Setup

1. Install the required NuGet packages:

   ```bash
   ./InstallPackages.sh
   ```

2. Update the `appsettings.json` with the PostgreSQL connection string.

3. Build and run the application:

   ```bash
   dotnet run
   ```

4. To run in Docker:
   ```bash
   docker build -t productcatalogservice .
   docker run -p 8080:80 productcatalogservice
   ```

## API Endpoints

- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get a product by ID
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

## Security

- All inputs are validated and sanitized.
- Uses SSL/TLS for data encryption.
- Follow best practices for securing secrets and keys.

## Notes

- The project follows SOLID principles, KISS, YAGNI, DRY, and clean code practices.
- Implemented with a functional programming paradigm, immutability, and stateless classes for performance and scalability.
