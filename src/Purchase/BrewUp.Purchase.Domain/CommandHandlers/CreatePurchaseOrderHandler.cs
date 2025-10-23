using BrewUp.Purchase.Domain.Commands;
using BrewUp.Purchase.Domain.Aggregates;

namespace BrewUp.Purchase.Domain.CommandHandlers;

public class CreatePurchaseOrderHandler() : CommandHandlerBaseAsync<CreatePurchaseOrder>
{
    public override async Task HandleAsync(CreatePurchaseOrder command, CancellationToken cancellationToken = default)
    {
        var order = new Order(command.PurchaseOrderId, command.BeerId, command.BeerName, command.Quantity);
        // Event sourcing - the aggregate will publish domain events
        await Task.CompletedTask;
    }
}