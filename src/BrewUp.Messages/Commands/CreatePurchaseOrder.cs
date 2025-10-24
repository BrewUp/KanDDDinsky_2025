using BrewUp.Purchase.SharedKernel.DomainIds;
using Muflone.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Messages.Commands;

public sealed class CreatePurchaseOrder(PurchaseOrderId aggregateId, string orderCode, Account who)
    : Command(aggregateId, who)
{
    public string OrderCode { get; } = orderCode;
}
