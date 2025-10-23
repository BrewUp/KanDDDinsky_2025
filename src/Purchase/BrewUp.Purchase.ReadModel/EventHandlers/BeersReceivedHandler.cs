using BrewUp.Purchase.Domain.Events;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.ReadModel.EventHandlers;

public class BeersReceivedHandler() : DomainEventHandlerBaseAsync<BeersReceived>
{
    public override async Task HandleAsync(BeersReceived domainEvent, CancellationToken cancellationToken = default)
    {
        // Implementazione per aggiornare il read model quando le birre vengono ricevute
        await Task.CompletedTask;
    }
}