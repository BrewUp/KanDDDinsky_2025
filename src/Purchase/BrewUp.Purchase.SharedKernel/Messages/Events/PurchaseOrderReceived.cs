using BrewUp.Purchase.SharedKernel.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.SharedKernel.Messages.Events;

public class PurchaseOrderReceived(PurchaseOrderId purchaseOrderId) : DomainEvent(purchaseOrderId)
{
    public PurchaseOrderId PurchaseOrderId { get; } = purchaseOrderId;
}