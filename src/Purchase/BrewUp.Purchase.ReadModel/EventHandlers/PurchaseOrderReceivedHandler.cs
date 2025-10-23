using BrewUp.Purchase.Domain.Events;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.EventHandlers;

public class PurchaseOrderReceivedHandler() : DomainEventHandlerBaseAsync<PurchaseOrderReceived>
{
    public override async Task HandleAsync(PurchaseOrderReceived domainEvent, CancellationToken cancellationToken = default)
    {
        // Implementazione per aggiornare il read model quando un ordine viene ricevuto
        await Task.CompletedTask;
    }
}