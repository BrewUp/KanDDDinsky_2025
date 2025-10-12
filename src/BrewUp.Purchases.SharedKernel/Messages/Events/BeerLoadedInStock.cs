using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Events;

namespace BrewUp.Purchases.SharedKernel.Messages.Events;

public sealed class BeerLoadedInStock(
    PurchaseOrderId aggregateId,
    IEnumerable<OrderLine> lines) : DomainEvent(aggregateId)
{
    public IEnumerable<OrderLine> Lines { get; } = lines;
}