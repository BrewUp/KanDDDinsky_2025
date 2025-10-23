using BrewUp.Purchase.SharedKernel.Messages.Commands;
using BrewUp.Purchase.Domain.Aggregates;

namespace BrewUp.Purchase.Domain.CommandHandlers;

public class LoadBeerInStockHandler() : CommandHandlerBaseAsync<LoadBeerInStock>
{
    public override async Task HandleAsync(LoadBeerInStock command, CancellationToken cancellationToken = default)
    {
        // Logic per caricare la birra in stock
        // Event sourcing - the aggregate will handle the business logic and publish domain events
        await Task.CompletedTask;
    }
}