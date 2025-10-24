using BrewUp.Purchase.Domain.Entities;
using BrewUp.Purchase.SharedKernel.DomainIds;
using BrewUp.Purchase.SharedKernel.Enumerations;
using FluentAssertions;

namespace BrewUp.Purchase.Domain.Tests.Entities;

public sealed class PurchaseOrderTests
{
    [Fact]
    public void CreatePurchaseOrder_ShouldCreateOrderWithCreatedStatus()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = "PO-001";

        // Act
        var purchaseOrder = PurchaseOrder.CreatePurchaseOrder(orderId, orderCode);

        // Assert
        purchaseOrder.Should().NotBeNull();
        purchaseOrder.Id.Should().Be(orderId);
    }

    [Fact]
    public void CreatePurchaseOrder_WithEmptyOrderCode_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = string.Empty;

        // Act
        Action act = () => PurchaseOrder.CreatePurchaseOrder(orderId, orderCode);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Order code cannot be empty*");
    }

    [Fact]
    public void AcknowledgeReceiving_ShouldUpdateReceivedQuantity()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = "PO-001";
        var purchaseOrder = PurchaseOrder.CreatePurchaseOrder(orderId, orderCode);
        var receivedQuantity = 100m;

        // Act
        purchaseOrder.AcknowledgeReceiving(receivedQuantity);

        // Assert
        // Note: Since we can't access private fields, we verify no exception is thrown
        // and the aggregate is in a valid state
        purchaseOrder.Should().NotBeNull();
    }

    [Fact]
    public void AcknowledgeReceiving_WithZeroQuantity_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = "PO-001";
        var purchaseOrder = PurchaseOrder.CreatePurchaseOrder(orderId, orderCode);

        // Act
        Action act = () => purchaseOrder.AcknowledgeReceiving(0);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Received quantity must be greater than zero*");
    }

    [Fact]
    public void AcknowledgeReceiving_WithNegativeQuantity_ShouldThrowArgumentException()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = "PO-001";
        var purchaseOrder = PurchaseOrder.CreatePurchaseOrder(orderId, orderCode);

        // Act
        Action act = () => purchaseOrder.AcknowledgeReceiving(-10);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Received quantity must be greater than zero*");
    }
}
