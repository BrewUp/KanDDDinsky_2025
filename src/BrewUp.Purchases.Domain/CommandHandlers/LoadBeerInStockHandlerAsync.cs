using BrewUp.Purchases.Domain.Entities;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public sealed class LoadBeerInStockHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<LoadBeerInStock>(repository, loggerFactory)
{
    public override async Task ProcessCommand(LoadBeerInStock command, CancellationToken cancellationToken = default)
    {
        var aggregate  = await Repository.GetByIdAsync<Order>(command.AggregateId, cancellationToken);
        aggregate!.LoadBeerToStock();

        Logger.LogInformation($"Beer loaded to stock for Order {aggregate.Id.Value}");
        await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }
}