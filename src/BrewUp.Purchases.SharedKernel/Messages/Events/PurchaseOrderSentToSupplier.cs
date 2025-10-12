using BrewUp.Purchases.SharedKernel.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Purchases.SharedKernel.Messages.Events;

public class PurchaseOrderSentToSupplier(PurchaseOrderId aggregateId, OrderDispatchDate dispatchDate)
    : DomainEvent(aggregateId)
{
    public OrderDispatchDate DispatchDate { get; } = dispatchDate;
}