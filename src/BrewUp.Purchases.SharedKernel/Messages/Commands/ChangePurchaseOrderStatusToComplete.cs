using BrewUp.Purchases.SharedKernel.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public class ChangePurchaseOrderStatusToComplete(PurchaseOrderId aggregateId) : Command(aggregateId);