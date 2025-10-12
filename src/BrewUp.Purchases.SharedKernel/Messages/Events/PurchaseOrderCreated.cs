using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Purchases.SharedKernel.Messages.Events;

public sealed class PurchaseOrderCreated(
	PurchaseOrderId aggregateId,
	SupplierId supplierId,
	DateTime date,
	IEnumerable<OrderLine> lines)
	: DomainEvent(aggregateId)
{
	public SupplierId SupplierId { get; } = supplierId;
	public DateTime Date { get; } = date;
	public IEnumerable<OrderLine> Lines { get; } = lines;
}