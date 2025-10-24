using BrewUp.Messages.Events;
using BrewUp.Purchase.ReadModel.Services;
using BrewUp.Purchase.SharedKernel.Enumerations;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.Events.Handlers;

public sealed class PurchaseOrderReceivedHandler(
    IPurchaseOrderService purchaseOrderService,
    ILoggerFactory loggerFactory)
    : DomainEventHandlerAsync<PurchaseOrderReceived>(loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<PurchaseOrderReceivedHandler>();

    public override async Task HandleAsync(PurchaseOrderReceived @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Handling PurchaseOrderReceived event: {AggregateId}, ReceivedQuantity: {ReceivedQuantity}",
            @event.AggregateId, @event.ReceivedQuantity);

        var orderId = Guid.Parse(@event.AggregateId.Value);

        await purchaseOrderService.UpdateReceivedQuantityAsync(
            orderId: orderId,
            receivedQuantity: @event.ReceivedQuantity,
            updatedAt: @event.Headers.When.Value,
            cancellationToken: cancellationToken);

        await purchaseOrderService.UpdateStatusAsync(
            orderId: orderId,
            newStatus: PurchaseOrderStatus.Received,
            updatedAt: @event.Headers.When.Value,
            cancellationToken: cancellationToken);

        // TODO: Integration events in Muflone need to be sent via RabbitMQ transport
        // This will be implemented when RabbitMQ configuration is added
        // var integrationEvent = new BeersReceived(
        //     orderId,
        //     @event.ReceivedQuantity,
        //     @event.Headers.When.Value);
        // await serviceBus.SendAsync(integrationEvent, cancellationToken);

        _logger.LogInformation("PurchaseOrderReceived event handled successfully: {AggregateId}", @event.AggregateId);
    }
}
