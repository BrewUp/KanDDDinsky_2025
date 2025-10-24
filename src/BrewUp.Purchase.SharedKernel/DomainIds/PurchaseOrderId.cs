using Muflone.Core;

namespace BrewUp.Purchase.SharedKernel.DomainIds;

public sealed class PurchaseOrderId(Guid value) : DomainId(value.ToString());
