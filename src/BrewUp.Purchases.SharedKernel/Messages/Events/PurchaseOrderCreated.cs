using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Purchases.SharedKernel.Messages.Events;

public sealed class PurchaseOrderCreated(
	PurchaseOrderId aggregateId,
	SupplierId supplierId,
	OrderCreateDate date,
	IEnumerable<OrderLine> lines)
	: DomainEvent(aggregateId)
{
	public SupplierId SupplierId { get; } = supplierId;
	public OrderCreateDate Date { get; } = date;
	public IEnumerable<OrderLine> Lines { get; } = lines;
}