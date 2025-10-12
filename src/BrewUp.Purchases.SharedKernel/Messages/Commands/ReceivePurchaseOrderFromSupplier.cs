using BrewUp.Purchases.SharedKernel.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public sealed class ReceivePurchaseOrderFromSupplier(PurchaseOrderId aggregateId) : Command(aggregateId)
{
}