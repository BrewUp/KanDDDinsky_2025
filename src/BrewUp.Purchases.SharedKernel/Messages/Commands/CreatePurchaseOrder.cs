using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public sealed class CreatePurchaseOrder(
	PurchaseOrderId aggregateId,
	SupplierId supplierId,
	OrderCreateDate date,
	IEnumerable<OrderLine> lines)
	: Command(aggregateId)
{
	public SupplierId SupplierId { get; } = supplierId;
	public OrderCreateDate Date { get; } = date;
	public IEnumerable<OrderLine> Lines { get; } = lines;
}