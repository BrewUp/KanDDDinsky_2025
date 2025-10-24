using BrewUp.Messages.Commands;
using BrewUp.Purchase.Domain.Commands.Handlers;
using BrewUp.Purchase.Domain.Entities;
using BrewUp.Purchase.SharedKernel.DomainIds;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Muflone.CustomTypes;
using Muflone.Persistence;

namespace BrewUp.Purchase.Domain.Tests.Commands.Handlers;

public sealed class AcknowledgeReceivingOrderHandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly AcknowledgeReceivingOrderHandler _handler;

    public AcknowledgeReceivingOrderHandlerTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _loggerFactoryMock = new Mock<ILoggerFactory>();

        var loggerMock = new Mock<ILogger<AcknowledgeReceivingOrderHandler>>();
        _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
            .Returns(loggerMock.Object);

        _handler = new AcknowledgeReceivingOrderHandler(_repositoryMock.Object, _loggerFactoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenOrderExists_ShouldAcknowledgeReceivingAndSave()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var receivedQuantity = 100m;
        var account = new Account("test@brewup.local", "Test User");
        var command = new AcknowledgeReceivingOrder(orderId, receivedQuantity, account);

        var existingOrder = PurchaseOrder.CreatePurchaseOrder(orderId, "PO-001");
        _repositoryMock.Setup(x => x.GetByIdAsync<PurchaseOrder>(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingOrder);

        // Act
        await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(
            x => x.SaveAsync(
                It.IsAny<PurchaseOrder>(),
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenOrderDoesNotExist_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var receivedQuantity = 100m;
        var account = new Account("test@brewup.local", "Test User");
        var command = new AcknowledgeReceivingOrder(orderId, receivedQuantity, account);

        _repositoryMock.Setup(x => x.GetByIdAsync<PurchaseOrder>(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PurchaseOrder?)null);

        // Act
        Func<Task> act = async () => await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Purchase order {orderId} not found");
    }
}
