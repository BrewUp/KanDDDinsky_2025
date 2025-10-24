using BrewUp.Purchase.Facade.BindingModels.v1;
using BrewUp.Purchase.Facade.BindingModels.v1.Input;
using Microsoft.Extensions.Logging;

namespace BrewUp.Purchase.Facade;

public sealed class PurchaseFacade : IPurchaseFacade
{
    private readonly ILogger<PurchaseFacade> _logger;
    private readonly Dictionary<Guid, PurchaseOrder> _inMemoryStore = new();

    public PurchaseFacade(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<PurchaseFacade>();
    }

    public Task<PurchaseOrder> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating purchase order with code: {OrderCode}", request.OrderCode);

        var purchaseOrder = new PurchaseOrder
        {
            Id = Guid.NewGuid(),
            OrderCode = request.OrderCode
        };

        _inMemoryStore[purchaseOrder.Id] = purchaseOrder;

        return Task.FromResult(purchaseOrder);
    }

    public Task AcknowledgeReceivingAsync(AcknowledgeReceivingRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Acknowledging receiving for order: {OrderId}", request.OrderId);

        if (!_inMemoryStore.ContainsKey(request.OrderId))
        {
            _logger.LogWarning("Purchase order not found: {OrderId}", request.OrderId);
            throw new KeyNotFoundException($"Purchase order with ID {request.OrderId} not found");
        }

        return Task.CompletedTask;
    }

    public Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving purchase order: {OrderId}", orderId);

        _inMemoryStore.TryGetValue(orderId, out var purchaseOrder);

        return Task.FromResult(purchaseOrder);
    }
}
