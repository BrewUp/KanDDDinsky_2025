using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public class CreatePurchaseOrder(
	PurchaseOrderId aggregateId,
	SupplierId supplierId,
	DateTime date,
	IEnumerable<OrderLine> lines)
	: Command(aggregateId)
{
	public SupplierId SupplierId { get; } = supplierId;
	public DateTime Date { get; } = date;
	public IEnumerable<OrderLine> Lines { get; } = lines;
}