using Brewup.Purchases.Domain.Entities;
using BrewUp.Purchases.Domain.Helpers;
using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Enums;
using BrewUp.Purchases.SharedKernel.Messages.Events;
using Muflone.Core;

namespace BrewUp.Purchases.Domain.Entities;

public class Order : AggregateRoot
{
	private SupplierId _supplierId;
	private OrderCreateDate _date;
	private IEnumerable<OrderLine> _lines;
	
	private OrderDispatchDate _dispatchDate;
	
	private Status _status;

	//Called when loaded from the event store
	protected Order()
	{
	}

	internal static Order Create(PurchaseOrderId id, SupplierId supplierId, OrderCreateDate date,
		IEnumerable<SharedKernel.Dtos.OrderLine> lines)
	{
		return new Order(id, supplierId, date, lines);
	}

	private Order(PurchaseOrderId id, SupplierId supplierId, OrderCreateDate date,
		IEnumerable<SharedKernel.Dtos.OrderLine> lines)
	{
		//Invariants checks
		//if (!_lines.Any())
		//	throw new ArgumentException("Order must have at least one line", nameof(lines));

		/////////
		RaiseEvent(new PurchaseOrderCreated(id, supplierId, date, lines));
	}

	private void Apply(PurchaseOrderCreated @event)
	{
		Id = @event.AggregateId;
		_status = Status.Created;
		//_supplierId = @event.SupplierId;
		//_date = @event.Date;
		_lines = @event.Lines.ToEntities();
	}
	
	internal void SendOrderToSupplier(OrderDispatchDate dispatchDate)
	{
		if (!_status.Equals(Status.Created))
			throw new InvalidOperationException(
				$"Cannot send order to supplier when status is {_status}");

		RaiseEvent(new PurchaseOrderSentToSupplier((PurchaseOrderId)Id, dispatchDate));
	}

	private void Apply(PurchaseOrderSentToSupplier @event)
	{
		_status = Status.Sent;
	}

	internal void Received()
	{
		if (!_status.Equals(Status.Complete))
			RaiseEvent(new PurchaseOrderStatusChangedToComplete((PurchaseOrderId)Id, _lines.ToDtos()));
	}

	private void Apply(PurchaseOrderStatusChangedToComplete @event)
	{
		_status = Status.Complete;
	}

	internal void LoadBeerToStock()
	{
		RaiseEvent(new BeerLoadedInStock((PurchaseOrderId)Id, _lines.ToDtos()));
	}
}