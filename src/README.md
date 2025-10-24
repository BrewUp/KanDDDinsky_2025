# BrewUp Purchase API

C# API infrastructure for the **Purchase** bounded context from the BrewUp ERP Miro diagram.

## Architecture

This solution follows **Domain-Driven Design (DDD)** principles with a modular monolith architecture:

```
src/
├── BrewUp.Purchase.Facade/              # Facade layer (interfaces, DTOs)
├── BrewUp.Purchase.Facade.Tests/        # Facade unit tests
├── BrewUp.Purchase.ArchitectureTests/   # NetArchTest architecture validation
├── BrewUp.PurchaseMediator/             # API endpoint definitions
└── BrewUp.Rest/                         # REST API entry point
    ├── Modules/
    │   ├── Common/                      # Infrastructure modules (Serilog, OpenAPI, etc.)
    │   ├── Contexts/                    # Domain context modules
    │   └── Mediators/                   # Endpoint registration modules
    └── Middleware/                      # Exception handling
```

## Project Structure

### Facade Layer (`BrewUp.Purchase.Facade`)
- **IPurchaseFacade**: Main facade interface
- **PurchaseFacade**: Stub implementation with in-memory storage
- **BindingModels/v1**: DTOs for API requests/responses
  - `PurchaseOrder`: Response DTO (Id, OrderCode)
  - `Input/CreatePurchaseOrderRequest`: Create order request
  - `Input/AcknowledgeReceivingRequest`: Acknowledge receiving request

### REST API (`BrewUp.Rest`)
Module-based architecture with auto-discovery:
- **IModule**: Core module abstraction
- **ModuleExtensions**: Auto-discovery and registration

### Common Modules (Order: 0-50)
1. **SerilogModule** (Order: 0) - Structured logging
2. **ExceptionHandlerModule** (Order: 0) - RFC 7807 ProblemDetails
3. **ScalarModule** (Order: 0) - OpenAPI + Scalar UI
4. **OpenTelemetryModule** (Order: 1) - Distributed tracing
5. **PurchaseModule** (Order: 10) - Facade registration
6. **HealthModule** (Order: 50) - Health checks
7. **PurchaseMediatorModule** (Order: 50) - Endpoint registration

## API Endpoints

Base URL: `http://localhost:5026`

### Purchase Orders (`/v1/purchase-orders`)

#### Create Purchase Order
```http
POST /v1/purchase-orders
Content-Type: application/json

{
  "orderCode": "PO-2025-001"
}
```

**Response**: `201 Created`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderCode": "PO-2025-001"
}
```

#### Get Purchase Order
```http
GET /v1/purchase-orders/{orderId}
```

**Response**: `200 OK`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "orderCode": "PO-2025-001"
}
```

#### Acknowledge Receiving
```http
POST /v1/purchase-orders/{orderId}/acknowledge
```

**Response**: `204 No Content`

### Health Check
```http
GET /health
```

**Response**: `200 OK`
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0010091",
  "entries": {}
}
```

## OpenAPI Documentation

### Scalar UI (Development only)
```
http://localhost:5026/scalar/v1
```

### OpenAPI JSON
```
http://localhost:5026/openapi/v1.json
```

## Configuration

### Serilog (`appsettings.json`)
- **Console sink**: Structured logging to console
- **File sink**: Rolling daily logs in `logs/logs_YYYYMMDD.log`
- **Retention**: 7 days

### OpenTelemetry
- **Service Name**: `brewup-purchase`
- **Tracing**: ASP.NET Core, HTTP Client
- **Metrics**: Runtime, ASP.NET Core, HTTP Client
- **Sampling**: 100% (configurable)

### Environment Configuration
- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development overrides (Debug logging)

## Build & Run

### Prerequisites
- .NET 9.0 SDK

### Build
```bash
cd src
dotnet restore
dotnet build --configuration Release
```

### Run Tests
```bash
dotnet test --configuration Release
```

**Test Results**:
- **Architecture Tests**: 5 tests (NetArchTest boundaries)
- **Facade Unit Tests**: 6 tests (Facade behavior)

### Run API
```bash
cd src/BrewUp.Rest
dotnet run
```

The API will start at `http://localhost:5026`

## Testing with curl

```bash
# Health check
curl -X GET http://localhost:5026/health

# Create purchase order
curl -X POST http://localhost:5026/v1/purchase-orders \
  -H "Content-Type: application/json" \
  -d '{"orderCode":"PO-2025-001"}'

# Get purchase order (replace {id} with actual ID)
curl -X GET http://localhost:5026/v1/purchase-orders/{id}

# Acknowledge receiving
curl -X POST http://localhost:5026/v1/purchase-orders/{id}/acknowledge
```

## Architecture Tests (NetArchTest)

Validates:
- ✅ Facade layer follows naming conventions
- ✅ Facade does not depend on Infrastructure
- ✅ Facade does not depend on REST API
- ✅ Interfaces start with 'I'
- ✅ Binding models are in correct namespace

## Technology Stack

- **.NET 9.0**
- **Minimal APIs** (ASP.NET Core)
- **Serilog** (Structured logging)
- **OpenTelemetry** (Distributed tracing)
- **Scalar** (OpenAPI documentation)
- **NetArchTest** (Architecture validation)
- **xUnit** (Testing framework)
- **Moq** (Mocking)
- **FluentAssertions** (Fluent test assertions)

## Key Features

✅ **Module-based architecture** with auto-discovery
✅ **OpenAPI + Scalar UI** for interactive documentation
✅ **Structured logging** with Serilog (Console + File)
✅ **OpenTelemetry** for distributed tracing
✅ **Health checks** endpoint
✅ **RFC 7807 ProblemDetails** error handling
✅ **Architecture tests** with NetArchTest
✅ **Path-based versioning** (`/v1`)
✅ **Treat warnings as errors**

## Future Extensions

This infrastructure is ready for:
- **Domain Layer**: Aggregates, Commands, Events, Handlers
- **Read Model Layer**: MongoDB integration, Event Handlers
- **Infrastructure Layer**: EventStore (Kurrent), PostgreSQL, RabbitMQ
- **Muflone Integration**: CQRS/Event Sourcing

Commands and Events will be implemented following the Miro diagram:
- `CreatePurchaseOrder` → `PurchaseOrderCreated`
- `AcknowledgeReceiving` → `PurchaseOrderReceived`, `BeersReceived` (Integration Event)
- `CompletePurchaseOrder` → `PurchaseOrderStatusChangedToComplete`

## License

This is an educational project for the KanDDDinsky 2025 workshop.
