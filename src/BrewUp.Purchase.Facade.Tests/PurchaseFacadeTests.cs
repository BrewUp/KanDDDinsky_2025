using BrewUp.Purchase.Facade.BindingModels.v1.Input;
using BrewUp.Purchase.Infrastructure.MongoDB;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Muflone.Persistence;

namespace BrewUp.Purchase.Facade.Tests;

public sealed class PurchaseFacadeTests
{
    private readonly Mock<IServiceBus> _serviceBusMock;
    private readonly Mock<IPurchaseOrderQueries> _purchaseOrderQueriesMock;
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly PurchaseFacade _sut;

    public PurchaseFacadeTests()
    {
        _serviceBusMock = new Mock<IServiceBus>();
        _purchaseOrderQueriesMock = new Mock<IPurchaseOrderQueries>();
        _loggerFactoryMock = new Mock<ILoggerFactory>();

        var loggerMock = new Mock<ILogger<PurchaseFacade>>();
        _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
            .Returns(loggerMock.Object);

        _sut = new PurchaseFacade(
            _serviceBusMock.Object,
            _purchaseOrderQueriesMock.Object,
            _loggerFactoryMock.Object);
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

        _purchaseOrderQueriesMock.Setup(x => x.GetByIdAsync(created.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SharedKernel.DTOs.PurchaseOrder
            {
                Id = created.Id,
                OrderCode = "PO-2025-001",
                Status = SharedKernel.Enumerations.PurchaseOrderStatus.Created,
                ReceivedQuantity = 0
            });

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

        _purchaseOrderQueriesMock.Setup(x => x.GetByIdAsync(created.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SharedKernel.DTOs.PurchaseOrder
            {
                Id = created.Id,
                OrderCode = "PO-2025-001",
                Status = SharedKernel.Enumerations.PurchaseOrderStatus.Created,
                ReceivedQuantity = 0
            });

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
