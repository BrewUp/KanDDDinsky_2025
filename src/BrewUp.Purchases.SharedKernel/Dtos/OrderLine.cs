using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;

namespace BrewUp.Purchases.SharedKernel.Dtos;

public class OrderLine
{
	public BeerId BeerId { get; set; } = null!;
	public BeerName BeerName { get; set; } = null!;
	public Quantity Quantity { get; set; } = null!;
	public Price Price { get; set; } = null!;
}