using Muflone.Core;

namespace BrewUp.Purchases.SharedKernel.CustomTypes;

public sealed class PurchaseOrderId(string value) : DomainId(value);