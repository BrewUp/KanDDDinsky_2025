using BrewUp.Purchases.SharedKernel.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public class SendPurchaseOrderToSupplier(PurchaseOrderId aggregateId, OrderDispatchDate dispatchDate)
    : Command(aggregateId)
{
    public OrderDispatchDate DispatchDate { get; } = dispatchDate;
}