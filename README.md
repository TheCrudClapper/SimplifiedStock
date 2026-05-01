# SimplifiedStock

A simplified stock market API implementation featuring wallets, a bank for stock inventory, and an audit log system. Built with .NET 10, Entity Framework Core, and PostgreSQL.

## Overview

SimplifiedStock simulates a basic stock exchange where:
- **Wallets** can own various stocks
- **Bank** controls stock inventory and executes buy/sell operations
- **Audit Log** tracks all wallet operations
- Stock price is fixed at 1 (no price fluctuation)
- All operations execute immediately

## Architecture

```
src
├── SimplifiedStock.API        - ASP.NET Core REST API
├── SimplifiedStock.Services   - Business logic & DTOs
├── SimplifiedStock.Domain     - Domain entities & enums
└── SimplifiedStock.Infrastructure - Data access & migrations
```

## Getting Started

### Prerequisites

- Docker
- Docker Compose
- Git

### Installation & Startup

#### Windows (PowerShell)

If no argument provided, default port is 8080

```powershell
./run-scripts/start.ps1 -port 8080
```

#### Linux / macOS (Bash)
```bash
chmod +x ./run-scripts/start.sh
./run-scripts/start.sh 8080
```

The application will be available at `http://localhost:8080`
Swagger UI: `http://localhost:8080/swagger`

### Stopping the Application

Press ENTER in the terminal where you started the application, or run:

```bash
docker compose down
```

## Architecture Overview

- **3 API Instances** with round-robin load balancing
- **Nginx** reverse proxy for request distribution
- **PostgreSQL** shared database for data consistency
- **Auto-restart** on failures

### Data Model

```
Wallet
├── Id (Guid - primary key)
└── WalletStocks[]
    ├── Id (Guid - part of composite key)
    ├── Name (string - part of composite key)
    ├── WalletId (Guid - foreign key)
    └── Quantity (int)

BankStock
├── Name (string - primary key)
└── Quantity (int)

AuditLog
├── Id (int - primary key)
├── WalletId (Guid)
├── Type (buy|sell)
├── StockName (string)
└── CreatedAt (DateTime)
```

## Example Workflow

```bash
# 1. Initialize bank with stocks
curl -X POST http://localhost:8080/stocks \
  -H "Content-Type: application/json" \
  -d '{
    "stocks": [
      {"name": "APPLE", "quantity": 100},
      {"name": "GOOGLE", "quantity": 50}
    ]
  }'

# 2. Check bank inventory
curl http://localhost:8080/stocks

# 3. Buy stock (creates wallet)
curl -X POST http://localhost:8080/wallets/550e8400-e29b-41d4-a716-446655440000/stocks/APPLE \
  -H "Content-Type: application/json" \
  -d '{"type":"buy"}'

# 4. Check wallet
curl http://localhost:8080/wallets/550e8400-e29b-41d4-a716-446655440000

# 5. Check wallet stock quantity
curl http://localhost:8080/wallets/550e8400-e29b-41d4-a716-446655440000/stocks/APPLE

# 6. Sell stock
curl -X POST http://localhost:8080/wallets/550e8400-e29b-41d4-a716-446655440000/stocks/APPLE \
  -H "Content-Type: application/json" \
  -d '{"type":"sell"}'

# 7. View audit log
curl http://localhost:8080/log
```

## Development

### Building Locally

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Database Migrations

Migrations run automatically on startup. To create a new migration:

```bash
dotnet ef migrations add <MigrationName> \
  -p src/SimplifiedStock.Infrastructure \
  -s src/SimplifiedStock.API
```

## Configuration

### Environment Variables

- `ASPNETCORE_URLS` - API endpoint (default: http://+:8080)
- `ConnectionStrings__Default` - PostgreSQL connection string
- `APP_PORT` - External port for docker compose (default: 8080)

## Troubleshooting

### Application won't start
```bash
# Check logs
docker compose logs api1

# Rebuild images
docker compose down
docker compose up --build
```

### Database migration errors
```bash
# Reset database
docker compose down -v
docker compose up --build
```

### Port already in use
```bash
# Use different port
./run-scripts/start.ps1 -port 8081
```

## Technologies

- **.NET 10** - Application framework
- **ASP.NET Core** - REST API
- **Entity Framework Core** - ORM
- **PostgreSQL** - Database
- **Nginx** - Load balancer
- **Docker** - Containerization
- **Swagger/OpenAPI** - API documentation