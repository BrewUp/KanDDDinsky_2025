using BrewUp.Purchase.SharedKernel.DomainIds;
using Muflone.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Messages.Commands;

public sealed class AcknowledgeReceivingOrder(PurchaseOrderId aggregateId, decimal receivedQuantity, Account who)
    : Command(aggregateId, who)
{
    public decimal ReceivedQuantity { get; } = receivedQuantity;
}
