using BrewUp.Purchase.Facade.BindingModels.v1.Input;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BrewUp.Purchase.Facade.Tests;

public sealed class PurchaseFacadeTests
{
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly PurchaseFacade _sut;

    public PurchaseFacadeTests()
    {
        _loggerFactoryMock = new Mock<ILoggerFactory>();
        var loggerMock = new Mock<ILogger<PurchaseFacade>>();
        _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
            .Returns(loggerMock.Object);

        _sut = new PurchaseFacade(_loggerFactoryMock.Object);
    }

    [Fact]
    public async Task CreatePurchaseOrderAsync_ShouldReturnPurchaseOrder_WithGeneratedId()
    {
        var request = new CreatePurchaseOrderRequest { OrderCode = "PO-2025-001" };

        var result = await _sut.CreatePurchaseOrderAsync(request);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.OrderCode.Should().Be("PO-2025-001");
    }

    [Fact]
    public async Task CreatePurchaseOrderAsync_ShouldGenerateDifferentIds_ForMultipleOrders()
    {
        var request1 = new CreatePurchaseOrderRequest { OrderCode = "PO-2025-001" };
        var request2 = new CreatePurchaseOrderRequest { OrderCode = "PO-2025-002" };

        var result1 = await _sut.CreatePurchaseOrderAsync(request1);
        var result2 = await _sut.CreatePurchaseOrderAsync(request2);

        result1.Id.Should().NotBe(result2.Id);
    }

    [Fact]
    public async Task GetPurchaseOrderByIdAsync_ShouldReturnPurchaseOrder_WhenExists()
    {
        var request = new CreatePurchaseOrderRequest { OrderCode = "PO-2025-001" };
        var created = await _sut.CreatePurchaseOrderAsync(request);

        var result = await _sut.GetPurchaseOrderByIdAsync(created.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(created.Id);
        result.OrderCode.Should().Be("PO-2025-001");
    }

    [Fact]
    public async Task GetPurchaseOrderByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        var nonExistentId = Guid.NewGuid();

        var result = await _sut.GetPurchaseOrderByIdAsync(nonExistentId);

        result.Should().BeNull();
    }

    [Fact]
    public async Task AcknowledgeReceivingAsync_ShouldSucceed_WhenOrderExists()
    {
        var createRequest = new CreatePurchaseOrderRequest { OrderCode = "PO-2025-001" };
        var created = await _sut.CreatePurchaseOrderAsync(createRequest);
        var acknowledgeRequest = new AcknowledgeReceivingRequest { OrderId = created.Id };

        var act = async () => await _sut.AcknowledgeReceivingAsync(acknowledgeRequest);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task AcknowledgeReceivingAsync_ShouldThrowKeyNotFoundException_WhenOrderNotExists()
    {
        var nonExistentId = Guid.NewGuid();
        var request = new AcknowledgeReceivingRequest { OrderId = nonExistentId };

        var act = async () => await _sut.AcknowledgeReceivingAsync(request);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Purchase order with ID {nonExistentId} not found");
    }
}
