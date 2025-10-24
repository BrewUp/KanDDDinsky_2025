using BrewUp.Purchase.SharedKernel.DTOs;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BrewUp.Purchase.Infrastructure.MongoDB;

public sealed class PurchaseOrderQueries(IMongoDatabase database, ILoggerFactory loggerFactory)
    : IPurchaseOrderQueries
{
    private readonly IMongoCollection<PurchaseOrder> _purchaseOrders = database.GetCollection<PurchaseOrder>("PurchaseOrders");
    private readonly ILogger<PurchaseOrderQueries> _logger = loggerFactory.CreateLogger<PurchaseOrderQueries>();

    public async Task<PurchaseOrder?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Querying purchase order by ID: {OrderId}", orderId);

        var filter = Builders<PurchaseOrder>.Filter.Eq(p => p.Id, orderId);
        var purchaseOrder = await _purchaseOrders.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return purchaseOrder;
    }

    public async Task<IEnumerable<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Querying all purchase orders");

        var purchaseOrders = await _purchaseOrders.Find(FilterDefinition<PurchaseOrder>.Empty)
            .ToListAsync(cancellationToken);

        return purchaseOrders;
    }
}
