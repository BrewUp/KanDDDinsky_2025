using BrewUp.Purchases.Domain.Entities;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public class ChangePurchaseOrderStatusToCompleteHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<ChangePurchaseOrderStatusToComplete>(repository, loggerFactory)
{
    public override async Task ProcessCommand(ChangePurchaseOrderStatusToComplete command, CancellationToken cancellationToken = default)
    {
        var aggregate  = await Repository.GetByIdAsync<Order>(command.AggregateId, cancellationToken);
        aggregate!.Complete();

        Logger.LogInformation($"Order status changed to completed for aggregateId: {aggregate.Id.Value}");
        await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }
}