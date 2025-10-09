# BrewUp API

BrewUp API is a .NET 9 application implementing a modular architecture based on Domain-Driven Design (DDD) for managing a brewery.

## Architecture

The application is organized into independent modules following DDD principles:

- **Purchase Module**: Manages the ingredient purchasing process
- **Warehouse Module**: Manages warehouse operations

### Solution Structure

```
src/
├── BrewUp.Rest/                          # Main REST API (90 Presentation)
│   ├── Modules/                          # Application modules
│   │   ├── IModule.cs                    # Base interface for modules
│   │   ├── ModuleExtensions.cs           # Extensions for module registration
│   │   ├── OpenApiModule.cs              # OpenAPI/Swagger configuration
│   │   ├── OpenTelemetryModule.cs        # Telemetry configuration
│   │   ├── PurchaseModule.cs             # Purchase module
│   │   └── WarehouseModule.cs            # Warehouse module
│   └── Program.cs                        # Application entry point
├── Purchase/                             # Purchase module (50 Modules)
│   ├── BrewUp.Purchase.Facade/           # Facade layer with endpoints
│   ├── BrewUp.Purchase.Domain/           # Domain logic
│   ├── BrewUp.Purchase.ReadModel/        # Queries and read models
│   ├── BrewUp.Purchase.SharedKernel/     # Shared module types
│   ├── BrewUp.Purchase.Infrastructure/   # Data access
│   └── BrewUp.Purchase.Tests/            # Architectural tests
├── Warehouse/                            # Warehouse module (50 Modules)
│   ├── BrewUp.Warehouse.Facade/          # Facade layer with endpoints
│   ├── BrewUp.Warehouse.Domain/          # Domain logic
│   ├── BrewUp.Warehouse.ReadModel/       # Queries and read models
│   ├── BrewUp.Warehouse.SharedKernel/    # Shared module types
│   ├── BrewUp.Warehouse.Infrastructure/  # Data access
│   └── BrewUp.Warehouse.Tests/           # Architectural tests
├── BrewUp.Shared/                        # Shared classes (30 Shared)
└── BrewUp.Infrastructure/                # Global infrastructure (80 Infrastructure)
```

## Technologies Used

- **.NET 9**: Core framework
- **Minimal APIs**: For REST endpoints
- **OpenAPI/Swagger**: API documentation
- **Scalar**: UI for API documentation
- **OpenTelemetry**: Telemetry and monitoring
- **Serilog**: Structured logging
- **NetArchTest**: Architectural tests
- **xUnit**: Test framework

## Running the Application

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or VS Code

### Local Execution

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd KanDDDinsky_2025/src
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Run the application:**
   ```bash
   cd BrewUp.Rest
   dotnet run
   ```

4. **Access API documentation:**
   - Scalar UI: `https://localhost:7000/scalar/v1`

### Available Endpoints

- **GET `/v1/Purchase`**: Status del modulo Purchase
- **GET `/v1/Warehouse`**: Status del modulo Warehouse

## Test

### Run all Tests

```bash
dotnet test
```

### Architectural Tests

Architectural tests verify:
- Isolation between modules (no cross-module dependencies)  
- Namespace compliance  
- DDD rules adherence

```bash
dotnet test --filter "Category=Architecture"
```

## Configuration

### Serilog

Logging is configured in `appsettings.json`:
- **Console**: Output to console
- **File**: Daily rolling logs in `Log/BrewUp.log`

### OpenTelemetry

Telemetry is configurable via `OpenTelemetryModule`:
- Disabled by default (`IsEnabled = false`)
- Supports Azure Monitor and OTLP

### Development Configurations

Development-specific settings are in `appsettings.Development.json` per l'ambiente di sviluppo.

## Modular Architecture

Each module implements:

1. **Facade Layer**: Exposes endpoints and manages integration
2. **Domain Layer**: Contains business logic
3. **Infrastructure Layer**: Data access and external services
4. **SharedKernel**: Types and interfaces shared within the module
5. **ReadModel**: Queries and read models
6. **Tests**: Architectural tests for module isolation

### Architectural Principles

- **Isolamento dei Moduli**: No module may depend on another
- **Facade Pattern**: Single entry point per module
- **DDD**: Separation between Domain, Infrastructure, and Application layers
- **API Versioning**: Path-based versioning (`/v1/`)

## Deployment

The application is designed for deployment on:
- **Azure Container Apps**
- **Azure App Service**
- **Container Docker**

### Security

- Prepared for Azure AD authentication
- JWT Bearer token support
- Configurable CORS

## Contributing

1. Respect the existing modular architecture
2. Run architectural tests before committing
3. Follow .NET naming conventions
4. Document APIs via OpenAPI

## Licensing

This project is licensed under the MIT License.