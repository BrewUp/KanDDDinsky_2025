using BrewUp.Messages.Commands;
using BrewUp.Purchase.Domain.Commands.Handlers;
using BrewUp.Purchase.SharedKernel.DomainIds;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Muflone.CustomTypes;
using Muflone.Persistence;

namespace BrewUp.Purchase.Domain.Tests.Commands.Handlers;

public sealed class CreatePurchaseOrderHandlerTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly Mock<ILoggerFactory> _loggerFactoryMock;
    private readonly CreatePurchaseOrderHandler _handler;

    public CreatePurchaseOrderHandlerTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _loggerFactoryMock = new Mock<ILoggerFactory>();

        var loggerMock = new Mock<ILogger<CreatePurchaseOrderHandler>>();
        _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>()))
            .Returns(loggerMock.Object);

        _handler = new CreatePurchaseOrderHandler(_repositoryMock.Object, _loggerFactoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldCreatePurchaseOrderAndSaveToRepository()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = "PO-001";
        var account = new Account("test@brewup.local", "Test User");
        var command = new CreatePurchaseOrder(orderId, orderCode, account);

        // Act
        await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(
            x => x.SaveAsync(
                It.IsAny<Domain.Entities.PurchaseOrder>(),
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WithValidCommand_ShouldNotThrowException()
    {
        // Arrange
        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var orderCode = "PO-002";
        var account = new Account("test@brewup.local", "Test User");
        var command = new CreatePurchaseOrder(orderId, orderCode, account);

        // Act
        Func<Task> act = async () => await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }
}
