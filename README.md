# BrewUp Purchase Management - Specification by Example Workshop

Welcome to this hands-on workshop for learning **Specification by Example** domain testing patterns in .NET with DDD and Event Sourcing!

This repository contains a partially implemented brewery purchase management system designed to help you master domain testing through practical exercises.

## 🎯 Learning Objectives

By completing these exercises, you will:

- **Master** the Specification by Example pattern for domain testing
- **Understand** how to test Event Sourcing aggregates effectively
- **Practice** writing Given-When-Then style tests
- **Learn** to express business requirements as executable specifications
- **Gain** experience with DDD tactical patterns testing

## 🏗️ System Overview

You're working with a brewery purchase management system that handles:

```text
📦 Purchase Orders Lifecycle:
┌─────────────┐    ┌──────────────┐    ┌─────────────┐    ┌─────────────┐
│   Create    │───▶│ Send to      │───▶│  Receive    │───▶│ Load Beer   │
│   Order     │    │ Supplier     │    │ from        │    │ in Stock    │
│             │    │              │    │ Supplier    │    │             │
└─────────────┘    └──────────────┘    └─────────────┘    └─────────────┘
```

### Domain Entities

- **Order**: Aggregate root managing the purchase order lifecycle
- **OrderLine**: Individual beer items with quantities and prices
- **Price**: Monetary value with currency
- **Quantity**: Amount with unit of measure

### Key Business Rules

1. Orders must have at least one order line
2. Orders can only be sent to supplier when in "Created" status
3. Orders can only be marked complete when not already complete
4. Beer can only be loaded to stock from completed orders
5. The system should handle idempotent operations gracefully

## 🧪 Exercise Structure

Each test follows the **Specification by Example** pattern with three key methods:

```csharp
protected override IEnumerable<DomainEvent> Given()
{
    // 🎭 ARRANGE: Set up the initial domain state
    // What events have already happened?
}

protected override CreatePurchaseOrder When()
{
    // 🎬 ACT: What command are we executing?
    // What business action is being performed?
}

protected override IEnumerable<DomainEvent> Expect()
{
    // ✅ ASSERT: What domain events should be raised?
    // What business outcomes do we expect?
}
```

## 📚 Exercises to Complete

### Exercise 1: Create Purchase Order ⭐

**Business Scenario**: A brewery manager wants to create a new purchase order for beer supplies.

**Your Task**: Implement the test `OrderCreatePurchaseOrderSuccessful`

**Given**: No existing purchase order (empty event history)
**When**: `CreatePurchaseOrder` command is executed with:

- Purchase Order ID
- Supplier ID  
- Order creation date
- List of beer order lines

**Expected Outcome**: `PurchaseOrderCreated` event should be raised

**Key Learning**: Testing aggregate creation from scratch

---

### Exercise 2: Send Order to Supplier ⭐⭐

**Business Scenario**: After creating an order, the brewery needs to send it to the supplier for fulfillment.

**Your Task**: Implement the test `PurchaseOrderSentToSupplierSuccessful`

**Given**: An existing purchase order (created state)
**When**: `SendPurchaseOrderToSupplier` command is executed
**Expected Outcome**: `PurchaseOrderSentToSupplier` event should be raised

**Key Learning**: Testing state transitions with preconditions

---

### Exercise 3: Receive Order from Supplier ⭐⭐

**Business Scenario**: The supplier has fulfilled the order and the brewery receives the beer shipment.

**Your Task**: Implement the test `OrderUpdatePurchaseOrderStatusToCompleteChangeStatus`

**Given**: A purchase order that exists but is not yet complete
**When**: `ReceivePurchaseOrderFromSupplier` command is executed  
**Expected Outcome**: `PurchaseOrderStatusChangedToComplete` event should be raised

**Key Learning**: Testing business state changes

---

### Exercise 4: Idempotent Operation Handling ⭐⭐⭐

**Business Scenario**: The system receives a duplicate "order received" notification and should handle it gracefully.

**Your Task**: Implement the test `OrderUpdatePurchaseOrderStatusToCompleteAlreadyCompleteDoesNothing`

**Given**: A purchase order that is **already complete**
**When**: `ReceivePurchaseOrderFromSupplier` command is executed again
**Expected Outcome**: **No events** should be raised (idempotent behavior)

**Key Learning**: Testing idempotency and business rule enforcement

---

### Exercise 5: Load Beer in Stock ⭐⭐⭐

**Business Scenario**: Once the order is received and verified, the beer needs to be added to the brewery's inventory.

**Your Task**: Implement the test `BeerLoadedInStockSuccessful`

**Given**: A complete purchase order (with full event history)
**When**: `LoadBeerInStock` command is executed
**Expected Outcome**: `BeerLoadedInStock` event should be raised

**Key Learning**: Testing complex aggregate state with multiple preconditions

## 🛠️ Getting Started

### Prerequisites

- .NET 9 SDK
- Visual Studio 2022 or VS Code with C# extension
- Basic understanding of C#, DDD concepts, and unit testing

### Setup Instructions

1. **Clone and explore**:

   ```bash
   git clone [repository-url]
   cd KanDDDinsky_2025/src
   ```

2. **Examine the domain**:
   - Review entities in `BrewUp.Purchases.Domain/Entities/`
   - Study command handlers in `BrewUp.Purchases.Domain/CommandHandlers/`
   - Understand events in `BrewUp.Purchases.SharedKernel/Messages/Events/`

3. **Run existing tests**:

   ```bash
   dotnet test BrewUp.Purchases.Domain.Tests
   ```

4. **Start with Exercise 1**: Open `OrderCreatePurchaseOrderSuccessful.cs`

### Test Implementation Tips

1. **Start with Given()**: What domain events represent the initial state?
2. **Define When()**: Create the command with appropriate test data
3. **Specify Expect()**: What domain events prove the business outcome?
4. **Use realistic data**: Create meaningful test data (beer names, quantities, prices)
5. **Think business-first**: Focus on business outcomes, not technical implementation

## 📖 Key Concepts to Master

### Specification by Example Benefits

- **Living Documentation**: Tests explain what the system does
- **Business Language**: Tests use domain terminology
- **Regression Protection**: Changes breaking business rules are caught immediately
- **Collaborative Tool**: Business and technical teams can review scenarios together

### Event Sourcing Testing Patterns

- **Given Events**: Represent the current state of the aggregate
- **Command Execution**: The business action being tested  
- **Expected Events**: The business outcomes that should occur
- **No Direct State Assertion**: Test through events, not internal aggregate state

### Common Pitfalls to Avoid

- ❌ Testing implementation details instead of business outcomes
- ❌ Creating tests that are too complex or test multiple scenarios
- ❌ Using technical language instead of business terminology
- ❌ Ignoring edge cases like idempotency and error conditions

## 🎯 Success Criteria

You'll know you've mastered the pattern when:

- ✅ All tests pass and clearly express business intent
- ✅ Test names read like business requirements
- ✅ Each test focuses on a single business scenario  
- ✅ Tests serve as documentation for the domain behavior
- ✅ Adding new scenarios follows the established pattern

## 🚀 Next Steps

After completing these exercises:

1. **Add Edge Cases**: Implement negative test scenarios (invalid commands, business rule violations)
2. **Extend Scenarios**: Add more complex business workflows
3. **Refactor for Clarity**: Improve test readability and maintainability  
4. **Team Review**: Share your solutions with colleagues for feedback

## 📚 Additional Resources

- **Domain-Driven Design** by Eric Evans
- **Specification by Example** by Gojko Adzic
- **Event Sourcing fundamentals** and CQRS patterns
- **Muflone Framework** documentation for .NET Event Sourcing

---

**Ready to start? Open your IDE and begin with Exercise 1! Remember: focus on expressing business intent clearly through your tests.** 🍺
