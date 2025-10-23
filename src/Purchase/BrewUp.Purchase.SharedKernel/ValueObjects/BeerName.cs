namespace BrewUp.Purchase.SharedKernel.ValueObjects;

public class BeerName(string value)
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(value));
    
    public static implicit operator BeerName(string value) => new(value);
    
    public static implicit operator string(BeerName beerName) => beerName.Value;
    
    public override string ToString() => Value;
    
    public override bool Equals(object? obj) => obj is BeerName other && Value == other.Value;
    
    public override int GetHashCode() => Value.GetHashCode();
}