using BrewUp.Purchase.Domain.Events;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.EventHandlers;

public class PurchaseOrderCreatedHandler() : DomainEventHandlerBaseAsync<PurchaseOrderCreated>
{
    public override async Task HandleAsync(PurchaseOrderCreated domainEvent, CancellationToken cancellationToken = default)
    {
        // Implementazione per aggiornare il read model quando un ordine di acquisto viene creato
        // Qui si potrebbe aggiornare una proiezione del database read-only
        await Task.CompletedTask;
    }
}