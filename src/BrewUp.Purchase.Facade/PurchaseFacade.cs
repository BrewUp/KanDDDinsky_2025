using BrewUp.Messages.Commands;
using BrewUp.Purchase.Facade.BindingModels.v1;
using BrewUp.Purchase.Facade.BindingModels.v1.Input;
using BrewUp.Purchase.Infrastructure.MongoDB;
using BrewUp.Purchase.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.CustomTypes;
using Muflone.Persistence;

namespace BrewUp.Purchase.Facade;

public sealed class PurchaseFacade(
    IServiceBus serviceBus,
    IPurchaseOrderQueries purchaseOrderQueries,
    ILoggerFactory loggerFactory) : IPurchaseFacade
{
    private readonly ILogger<PurchaseFacade> _logger = loggerFactory.CreateLogger<PurchaseFacade>();

    public async Task<PurchaseOrder> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating purchase order with code: {OrderCode}", request.OrderCode);

        var orderId = new PurchaseOrderId(Guid.NewGuid());
        var account = new Account("system@brewup.local", "System"); // TODO: Get from authenticated user

        var command = new CreatePurchaseOrder(orderId, request.OrderCode, account);
        await serviceBus.SendAsync(command, cancellationToken);

        _logger.LogInformation("Purchase order created successfully: {OrderId}", orderId);

        // Return the created order
        // Note: In a real scenario with event sourcing, you might need to wait for the read model to be updated
        // or use eventual consistency patterns
        return new PurchaseOrder
        {
            Id = Guid.Parse(orderId.Value),
            OrderCode = request.OrderCode
        };
    }

    public async Task AcknowledgeReceivingAsync(AcknowledgeReceivingRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Acknowledging receiving for order: {OrderId}, Quantity: {ReceivedQuantity}",
            request.OrderId, request.ReceivedQuantity);

        var existingOrder = await purchaseOrderQueries.GetByIdAsync(request.OrderId, cancellationToken);
        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Purchase order with ID {request.OrderId} not found");
        }

        var orderId = new PurchaseOrderId(request.OrderId);
        var account = new Account("system@brewup.local", "System"); // TODO: Get from authenticated user

        var command = new AcknowledgeReceivingOrder(orderId, request.ReceivedQuantity, account);
        await serviceBus.SendAsync(command, cancellationToken);

        _logger.LogInformation("Purchase order receiving acknowledged successfully: {OrderId}", orderId);
    }

    public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving purchase order: {OrderId}", orderId);

        var dto = await purchaseOrderQueries.GetByIdAsync(orderId, cancellationToken);

        if (dto == null)
        {
            _logger.LogWarning("Purchase order not found: {OrderId}", orderId);
            return null;
        }

        return new PurchaseOrder
        {
            Id = dto.Id,
            OrderCode = dto.OrderCode,
            Status = dto.Status.ToString(),
            ReceivedQuantity = dto.ReceivedQuantity
        };
    }
}
