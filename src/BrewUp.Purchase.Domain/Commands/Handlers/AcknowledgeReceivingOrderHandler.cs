using BrewUp.Messages.Commands;
using BrewUp.Purchase.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace BrewUp.Purchase.Domain.Commands.Handlers;

public sealed class AcknowledgeReceivingOrderHandler(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerAsync<AcknowledgeReceivingOrder>(repository, loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<AcknowledgeReceivingOrderHandler>();

    public override async Task HandleAsync(AcknowledgeReceivingOrder command, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processing command: AcknowledgeReceivingOrder - AggregateId: {AggregateId}, ReceivedQuantity: {ReceivedQuantity}",
            command.AggregateId, command.ReceivedQuantity);

        var purchaseOrder = await Repository.GetByIdAsync<Entities.PurchaseOrder>(command.AggregateId, cancellationToken)
            ?? throw new InvalidOperationException($"Purchase order {command.AggregateId} not found");

        purchaseOrder.AcknowledgeReceiving(command.ReceivedQuantity);

        await Repository.SaveAsync(purchaseOrder, Guid.NewGuid(), cancellationToken);

        _logger.LogInformation("Purchase order receiving acknowledged successfully: {AggregateId}", command.AggregateId);
    }
}
