using BrewUp.Purchase.SharedKernel.ValueObjects;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.Domain.Events;

public class PurchaseOrderReceived(PurchaseOrderId purchaseOrderId) : DomainEvent(purchaseOrderId)
{
    public PurchaseOrderId PurchaseOrderId { get; } = purchaseOrderId;
}