using BrewUp.Purchase.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Messages.Events;

public sealed class PurchaseOrderReceived(PurchaseOrderId aggregateId, decimal receivedQuantity)
    : DomainEvent(aggregateId)
{
    public decimal ReceivedQuantity { get; } = receivedQuantity;
}
