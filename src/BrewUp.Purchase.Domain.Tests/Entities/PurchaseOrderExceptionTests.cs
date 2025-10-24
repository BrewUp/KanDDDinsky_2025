using BrewUp.Purchase.Domain.Entities;
using BrewUp.Purchase.SharedKernel.DomainIds;
using BrewUp.Purchase.SharedKernel.Enumerations;
using FluentAssertions;

namespace BrewUp.Purchase.Domain.Tests.Entities;

public sealed class PurchaseOrderExceptionTests
{
    [Fact]
    public void CreatePurchaseOrder_WithEmptyCode_ThrowsArgumentException()
    {
        // Arrange
        var purchaseOrderId = new PurchaseOrderId(Guid.NewGuid());
        var emptyCode = string.Empty;

        // Act
        var act = () => PurchaseOrder.CreatePurchaseOrder(purchaseOrderId, emptyCode);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Order code cannot be empty*")
            .WithParameterName("orderCode");
    }

    [Fact]
    public void CreatePurchaseOrder_WithNullCode_ThrowsArgumentException()
    {
        // Arrange
        var purchaseOrderId = new PurchaseOrderId(Guid.NewGuid());
        string? nullCode = null;

        // Act
        var act = () => PurchaseOrder.CreatePurchaseOrder(purchaseOrderId, nullCode!);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Order code cannot be empty*")
            .WithParameterName("orderCode");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100.5)]
    public void AcknowledgeReceiving_WithInvalidQuantity_ThrowsArgumentException(decimal invalidQuantity)
    {
        // Arrange
        var purchaseOrderId = new PurchaseOrderId(Guid.NewGuid());
        var purchaseOrder = PurchaseOrder.CreatePurchaseOrder(purchaseOrderId, "PO-2025-001");

        // Act
        var act = () => purchaseOrder.AcknowledgeReceiving(invalidQuantity);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Received quantity must be greater than zero*")
            .WithParameterName("receivedQuantity");
    }

    [Fact]
    public void AcknowledgeReceiving_WithWrongStatus_ThrowsInvalidOperationException()
    {
        // Arrange
        var purchaseOrderId = new PurchaseOrderId(Guid.NewGuid());
        var purchaseOrder = PurchaseOrder.CreatePurchaseOrder(purchaseOrderId, "PO-2025-001");

        // First acknowledge to change status to Complete
        purchaseOrder.AcknowledgeReceiving(100m);

        // Act - Try to acknowledge again
        var act = () => purchaseOrder.AcknowledgeReceiving(50m);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage($"Cannot acknowledge receiving for order in status {PurchaseOrderStatus.Complete}");
    }
}
