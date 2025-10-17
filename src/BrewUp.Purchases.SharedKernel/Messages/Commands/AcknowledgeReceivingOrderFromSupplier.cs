using BrewUp.Purchases.SharedKernel.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public sealed class AcknowledgeReceivingOrderFromSupplier(PurchaseOrderId aggregateId) 
    : Command(aggregateId)
{
}