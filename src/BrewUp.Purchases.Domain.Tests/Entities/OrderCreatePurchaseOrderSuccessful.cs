using Brewup.Purchases.Domain.CommandHandlers;
using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using BrewUp.Purchases.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Purchases.Domain.Tests.Entities;

public class OrderCreatePurchaseOrderSuccessful : CommandSpecification<CreatePurchaseOrder>
{
	private readonly PurchaseOrderId _purchaseOrderId;
	private readonly SupplierId _supplierId;

	private readonly OrderCreateDate _date;

	private readonly IEnumerable<OrderLine> _lines;

	public OrderCreatePurchaseOrderSuccessful()
	{
		_purchaseOrderId = new PurchaseOrderId(Guid.NewGuid().ToString());
		_supplierId = new SupplierId(Guid.NewGuid().ToString());
		_date = new OrderCreateDate(DateTime.Today);

		_lines = [];
		_lines = _lines.Concat(new List<OrderLine>
		{
			new()
			{
				BeerId = new BeerId(Guid.NewGuid().ToString()),
				BeerName = new BeerName("Product 1"),
				Quantity = new Quantity {UnitOfMeasure = "N.", Value = 1},
				Price = new Price {Currency = "EUR", Value = 1}
			},
			new()
			{
				BeerId = new BeerId(Guid.NewGuid().ToString()),
				BeerName = new BeerName("Product 2"),
				Quantity = new Quantity {UnitOfMeasure = "N.", Value = 2},
				Price = new Price {Currency = "EUR", Value = 2}
			}
		});
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreatePurchaseOrder When()
	{
		return new CreatePurchaseOrder(_purchaseOrderId, _supplierId, _date, _lines);
	}

	protected override ICommandHandlerAsync<CreatePurchaseOrder> OnHandler()
	{
		return new CreatePurchaseOrderHandlerAsync(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new PurchaseOrderCreated(_purchaseOrderId, _supplierId, _date, _lines);
	}
}