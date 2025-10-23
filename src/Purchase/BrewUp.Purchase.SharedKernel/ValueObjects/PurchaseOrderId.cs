using Muflone.Core;

namespace BrewUp.Purchase.SharedKernel.ValueObjects;

public class PurchaseOrderId(Guid value) : DomainId(value.ToString())
{
    public Guid Value { get; } = value;
    
    public static PurchaseOrderId NewId() => new(Guid.NewGuid());
    
    public static implicit operator PurchaseOrderId(Guid value) => new(value);
    
    public static implicit operator Guid(PurchaseOrderId purchaseOrderId) => purchaseOrderId.Value;
}