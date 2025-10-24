using BrewUp.Purchase.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Messages.Events;

public sealed class PurchaseOrderCreated(PurchaseOrderId aggregateId, string orderCode)
    : DomainEvent(aggregateId)
{
    public string OrderCode { get; } = orderCode;
}
