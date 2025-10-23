using Muflone.Core;

namespace BrewUp.Purchase.SharedKernel.ValueObjects;

public class BeerId(Guid value) : DomainId(value.ToString())
{
    public new Guid Value { get; } = value;
    
    public static BeerId NewId() => new(Guid.NewGuid());
    
    public static implicit operator BeerId(Guid value) => new(value);
    
    public static implicit operator Guid(BeerId beerId) => beerId.Value;
}