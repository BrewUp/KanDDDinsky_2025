namespace BrewUp.Purchase.SharedKernel.CustomTypes;

public class Quantity(int value)
{
    public int Value { get; } = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Quantity must be non-negative");
    
    public static implicit operator Quantity(int value) => new(value);
    
    public static implicit operator int(Quantity quantity) => quantity.Value;
    
    public override string ToString() => Value.ToString();
    
    public override bool Equals(object? obj) => obj is Quantity other && Value == other.Value;
    
    public override int GetHashCode() => Value.GetHashCode();
}