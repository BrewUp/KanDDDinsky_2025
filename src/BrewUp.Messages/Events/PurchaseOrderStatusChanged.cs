using BrewUp.Purchase.SharedKernel.DomainIds;
using BrewUp.Purchase.SharedKernel.Enumerations;
using Muflone.Messages.Events;

namespace BrewUp.Messages.Events;

public sealed class PurchaseOrderStatusChanged(PurchaseOrderId aggregateId, PurchaseOrderStatus newStatus)
    : DomainEvent(aggregateId)
{
    public PurchaseOrderStatus NewStatus { get; } = newStatus;
}
