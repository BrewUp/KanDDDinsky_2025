namespace BrewUp.Purchases.SharedKernel.Dtos;

public class Price
{
	public decimal Value { get; set; }
	public string Currency { get; set; } = string.Empty;
}