namespace BrewUp.Purchases.SharedKernel.Dtos;

public class Quantity
{
	public decimal Value { get; set; }
	public string UnitOfMeasure { get; set; } = string.Empty;
}