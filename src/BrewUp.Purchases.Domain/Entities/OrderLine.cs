using BrewUp.Purchases.SharedKernel.CustomTypes;
using Muflone.Core;

namespace Brewup.Purchases.Domain.Entities;

public class OrderLine(BeerId beerId, string title, Quantity quantity, Price price)
	: Entity
{
	public BeerId BeerId { get; } = beerId;
	public string Title { get; } = title;
	public Quantity Quantity { get; } = quantity;
	public Price Price { get; } = price;
}