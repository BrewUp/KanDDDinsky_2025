# BrewUp Purchase Management - Specification by Example Domain Testing

This solution demonstrates how to implement domain tests using the **Specification by Example** pattern in a .NET 9 DDD (Domain-Driven Design) application. The project showcases a purchase order management system for a brewery, focusing on comprehensive domain testing strategies.

## 🎯 Purpose

The main goal of this solution is to show how to implement robust domain tests using the Specification by Example pattern, which provides:

- **Living Documentation**: Tests serve as executable specifications
- **Clear Business Intent**: Test names and structure reflect business scenarios
- **Behavior-Driven Testing**: Focus on business outcomes rather than implementation details
- **Maintainable Test Suite**: Easy to understand and modify test cases

## 🏗️ Architecture Overview

The solution follows Clean Architecture principles with DDD tactical patterns:

```text
├── 🏛️  Domain Layer
│   ├── Entities (Order, OrderLine, Price, Quantity)
│   ├── Command Handlers
│   └── Domain Logic
├── 🔄 SharedKernel
│   ├── Commands & Events
│   ├── Custom Types
│   └── DTOs
├── 📊 ReadModel
├── 🌐 REST API
└── ✅ Domain Tests (Specification by Example)
```

## 🧪 Specification by Example Pattern

### What is Specification by Example?

Specification by Example is a collaborative approach where:

- **Business requirements** are captured as concrete examples
- **Tests are written** using the **Given-When-Then** structure
- **Tests serve as living documentation** of the system behavior
- **Business and technical teams** collaborate on defining specifications

### Implementation Structure

Each test follows the pattern:

```csharp
public class OrderCreatePurchaseOrderSuccessful : CommandSpecification<CreatePurchaseOrder>
{
    // Test setup and data preparation
    
    protected override IEnumerable<DomainEvent> Given()
    {
        // Arrange: Set up the initial state
    }
    
    protected override CreatePurchaseOrder When()
    {
        // Act: Execute the command
    }
    
    protected override IEnumerable<DomainEvent> Expect()
    {
        // Assert: Verify expected domain events
    }
}
```

## 📋 Business Scenarios Covered

### 1. Purchase Order Creation

- **Scenario**: Creating a new purchase order with multiple beer items
- **Test**: `OrderCreatePurchaseOrderSuccessful`
- **Given**: No existing order
- **When**: Create purchase order command is executed
- **Then**: PurchaseOrderCreated event is raised

### 2. Order Dispatch

- **Scenario**: Sending a purchase order to supplier
- **Test**: `PurchaseOrderSentToSupplierSuccessful`
- **Given**: An existing created purchase order
- **When**: Send order to supplier command is executed
- **Then**: PurchaseOrderSentToSupplier event is raised

### 3. Order Completion

- **Scenario**: Receiving order from supplier
- **Test**: `OrderUpdatePurchaseOrderStatusToCompleteChangeStatus`
- **Given**: An existing purchase order
- **When**: Receive order command is executed
- **Then**: PurchaseOrderStatusChangedToComplete event is raised

### 4. Idempotency Handling

- **Scenario**: Attempting to complete an already completed order
- **Test**: `OrderUpdatePurchaseOrderStatusToCompleteAlreadyCompleteDoesNothing`
- **Given**: A purchase order that is already complete
- **When**: Receive order command is executed
- **Then**: No events are raised (idempotent behavior)

### 5. Stock Loading

- **Scenario**: Loading received beer into stock
- **Test**: `BeerLoadedInStockSuccessful`
- **Given**: A completed purchase order
- **When**: Load beer in stock command is executed
- **Then**: BeerLoadedInStock event is raised

## 🔧 Technical Implementation

### Test Base Class

Tests inherit from `CommandSpecification<T>` which provides:

- **Event Sourcing simulation** through Given events
- **Command execution** infrastructure
- **Event verification** mechanisms
- **Repository mocking** and setup

### Domain Events Verification

Each test verifies that the correct domain events are raised:

```csharp
protected override IEnumerable<DomainEvent> Expect()
{
    yield return new PurchaseOrderCreated(_purchaseOrderId, _supplierId, _date, _lines);
}
```

### Test Data Management

Tests use **Builder Pattern** concepts for creating test data:

- Consistent data setup across tests
- Realistic business scenarios
- Easy to maintain and modify

## 🚀 Running the Tests

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or VS Code
- xUnit test runner

### Execute Tests

```bash
# Navigate to the solution directory
cd src

# Run all domain tests
dotnet test BrewUp.Purchases.Domain.Tests

# Run specific test
dotnet test --filter "DisplayName~OrderCreatePurchaseOrderSuccessful"

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 📖 Benefits of This Approach

### 1. **Living Documentation**

- Test names clearly describe business scenarios
- Tests serve as up-to-date specification documentation
- New team members can understand business rules by reading tests

### 2. **Regression Safety**

- Domain logic changes are immediately caught
- Business rule violations are prevented
- Refactoring is safer with comprehensive test coverage

### 3. **Collaboration Enhancement**

- Business stakeholders can review and validate test scenarios
- Tests bridge the gap between business and technical teams
- Requirements are expressed in executable format

### 4. **Maintainable Test Suite**

- Clear separation of test concerns
- Consistent test structure across the domain
- Easy to add new scenarios or modify existing ones

## 🛠️ Key Technologies

- **.NET 9**: Latest .NET framework
- **Muflone**: CQRS/Event Sourcing framework
- **xUnit**: Testing framework
- **Muflone.SpecificationTests**: Specification testing infrastructure

## 📚 Domain Model

### Core Entities

- **Order**: Aggregate root managing purchase order lifecycle
- **OrderLine**: Value object representing individual beer items
- **Price**: Value object for monetary amounts
- **Quantity**: Value object for quantities with units

### Domain Events

- `PurchaseOrderCreated`
- `PurchaseOrderSentToSupplier`
- `PurchaseOrderStatusChangedToComplete`
- `BeerLoadedInStock`

### Commands

- `CreatePurchaseOrder`
- `SendPurchaseOrderToSupplier`
- `ReceivePurchaseOrderFromSupplier`
- `LoadBeerInStock`

## 🎓 Learning Outcomes

By studying this solution, you'll learn:

1. **How to structure domain tests** using Specification by Example
2. **Event Sourcing testing patterns** and verification strategies
3. **DDD tactical patterns** implementation in .NET
4. **CQRS command handling** with comprehensive testing
5. **Business scenario modeling** through executable specifications

## 🤝 Contributing

This is a learning and demonstration project. Feel free to:

- Add new test scenarios
- Improve existing test coverage
- Enhance domain model complexity
- Extend business rules implementation

---

*This solution serves as a practical example of implementing Specification by Example in .NET applications with DDD and Event Sourcing patterns.*
