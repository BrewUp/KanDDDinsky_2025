using BrewUp.Purchase.Domain.Events;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.EventHandlers;

public class BeerLoadedInStockHandler() : DomainEventHandlerBaseAsync<BeerLoadedInStock>
{
    public override async Task HandleAsync(BeerLoadedInStock domainEvent, CancellationToken cancellationToken = default)
    {
        // Implementazione per aggiornare il read model quando una birra viene caricata in stock
        await Task.CompletedTask;
    }
}