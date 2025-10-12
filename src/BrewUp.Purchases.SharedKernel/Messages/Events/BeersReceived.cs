using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Purchases.SharedKernel.Messages.Events;

public sealed class BeersReceived(PurchaseOrderId aggregateId, Guid correlationId, IEnumerable<OrderLine> orderLines)
	: IntegrationEvent(aggregateId, correlationId)
{
	public readonly PurchaseOrderId BuyOrderId = aggregateId;
	public readonly IEnumerable<OrderLine> OrderLines = orderLines;
}