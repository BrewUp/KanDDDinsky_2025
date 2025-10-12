using BrewUp.Purchases.SharedKernel.Dtos;

namespace BrewUp.Purchases.Domain.Helpers;

public static class Helpers
{
	public static IEnumerable<OrderLine> ToDtos(this IEnumerable<Brewup.Purchases.Domain.Entities.OrderLine> lines)
	{
		return lines.Select(x => new OrderLine
		{
			BeerId = x.BeerId,
			BeerName = new BeerName(x.Title),
			Price = new Price { Currency = x.Price.Currency, Value = x.Price.Value },
			Quantity = new Quantity { UnitOfMeasure = x.Quantity.UnitOfMeasure, Value = x.Quantity.Value }
		}).ToList();
	}

	public static IEnumerable<Brewup.Purchases.Domain.Entities.OrderLine> ToEntities(this IEnumerable<OrderLine> lines)
	{
		return lines.Select(x => new Brewup.Purchases.Domain.Entities.OrderLine(
			x.BeerId,
			x.BeerName.Value,
			new Brewup.Purchases.Domain.Entities.Quantity(x.Quantity.Value, x.Quantity.UnitOfMeasure),
			new Brewup.Purchases.Domain.Entities.Price(x.Price.Value, x.Price.Currency))).ToList();
	}
}