using BrewUp.Messages.Events;
using BrewUp.Purchase.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.Events.Handlers;

public sealed class PurchaseOrderStatusChangedHandler(
    IPurchaseOrderService purchaseOrderService,
    ILoggerFactory loggerFactory)
    : DomainEventHandlerAsync<PurchaseOrderStatusChanged>(loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<PurchaseOrderStatusChangedHandler>();

    public override async Task HandleAsync(PurchaseOrderStatusChanged @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Handling PurchaseOrderStatusChanged event: {AggregateId}, NewStatus: {NewStatus}",
            @event.AggregateId, @event.NewStatus);

        await purchaseOrderService.UpdateStatusAsync(
            orderId: Guid.Parse(@event.AggregateId.Value),
            newStatus: @event.NewStatus,
            updatedAt: @event.Headers.When.Value,
            cancellationToken: cancellationToken);

        _logger.LogInformation("PurchaseOrderStatusChanged event handled successfully: {AggregateId}", @event.AggregateId);
    }
}
