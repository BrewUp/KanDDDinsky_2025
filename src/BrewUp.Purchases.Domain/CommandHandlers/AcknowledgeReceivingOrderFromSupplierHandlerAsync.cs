using BrewUp.Purchases.Domain.Entities;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public class AcknowledgeReceivingOrderFromSupplierHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<AcknowledgeReceivingOrderFromSupplier>(repository, loggerFactory)
{
    public override async Task ProcessCommand(AcknowledgeReceivingOrderFromSupplier command, CancellationToken cancellationToken = default)
    {
        var aggregate  = await Repository.GetByIdAsync<Order>(command.AggregateId, cancellationToken);
        aggregate!.Received();

        Logger.LogInformation($"Order status changed to completed for aggregateId: {aggregate.Id.Value}");
        await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }
}