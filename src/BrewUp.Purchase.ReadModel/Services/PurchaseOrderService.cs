using BrewUp.Purchase.SharedKernel.DTOs;
using BrewUp.Purchase.SharedKernel.Enumerations;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BrewUp.Purchase.ReadModel.Services;

public sealed class PurchaseOrderService(IMongoDatabase database, ILoggerFactory loggerFactory)
    : IPurchaseOrderService
{
    private readonly IMongoCollection<PurchaseOrder> _purchaseOrders = database.GetCollection<PurchaseOrder>("PurchaseOrders");
    private readonly ILogger<PurchaseOrderService> _logger = loggerFactory.CreateLogger<PurchaseOrderService>();

    public async Task CreatePurchaseOrderAsync(Guid orderId, string orderCode, PurchaseOrderStatus status, DateTime createdAt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating purchase order read model: {OrderId}, OrderCode: {OrderCode}", orderId, orderCode);

        var purchaseOrder = new PurchaseOrder
        {
            Id = orderId,
            OrderCode = orderCode,
            Status = status,
            ReceivedQuantity = 0,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };

        await _purchaseOrders.InsertOneAsync(purchaseOrder, cancellationToken: cancellationToken);

        _logger.LogInformation("Purchase order read model created: {OrderId}", orderId);
    }

    public async Task UpdateReceivedQuantityAsync(Guid orderId, decimal receivedQuantity, DateTime updatedAt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating received quantity for purchase order: {OrderId}, Quantity: {ReceivedQuantity}", orderId, receivedQuantity);

        var filter = Builders<PurchaseOrder>.Filter.Eq(p => p.Id, orderId);
        var update = Builders<PurchaseOrder>.Update
            .Set(p => p.ReceivedQuantity, receivedQuantity)
            .Set(p => p.UpdatedAt, updatedAt);

        await _purchaseOrders.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        _logger.LogInformation("Purchase order received quantity updated: {OrderId}", orderId);
    }

    public async Task UpdateStatusAsync(Guid orderId, PurchaseOrderStatus newStatus, DateTime updatedAt, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating status for purchase order: {OrderId}, NewStatus: {NewStatus}", orderId, newStatus);

        var filter = Builders<PurchaseOrder>.Filter.Eq(p => p.Id, orderId);
        var update = Builders<PurchaseOrder>.Update
            .Set(p => p.Status, newStatus)
            .Set(p => p.UpdatedAt, updatedAt);

        await _purchaseOrders.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        _logger.LogInformation("Purchase order status updated: {OrderId}", orderId);
    }

    public async Task<PurchaseOrder?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving purchase order: {OrderId}", orderId);

        var filter = Builders<PurchaseOrder>.Filter.Eq(p => p.Id, orderId);
        var purchaseOrder = await _purchaseOrders.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return purchaseOrder;
    }
}
