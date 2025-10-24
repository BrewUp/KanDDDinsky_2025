using BrewUp.Messages.Events;
using BrewUp.Purchase.ReadModel.Services;
using BrewUp.Purchase.SharedKernel.Enumerations;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.Events.Handlers;

public sealed class PurchaseOrderCreatedHandler(
    IPurchaseOrderService purchaseOrderService,
    ILoggerFactory loggerFactory)
    : DomainEventHandlerAsync<PurchaseOrderCreated>(loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<PurchaseOrderCreatedHandler>();

    public override async Task HandleAsync(PurchaseOrderCreated @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Handling PurchaseOrderCreated event: {AggregateId}, OrderCode: {OrderCode}",
            @event.AggregateId, @event.OrderCode);

        await purchaseOrderService.CreatePurchaseOrderAsync(
            orderId: Guid.Parse(@event.AggregateId.Value),
            orderCode: @event.OrderCode,
            status: PurchaseOrderStatus.Created,
            createdAt: @event.Headers.When.Value,
            cancellationToken: cancellationToken);

        _logger.LogInformation("PurchaseOrderCreated event handled successfully: {AggregateId}", @event.AggregateId);
    }
}
