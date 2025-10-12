using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using Muflone.Messages.Commands;

namespace BrewUp.Purchases.SharedKernel.Messages.Commands;

public sealed class LoadBeerInStock(
    PurchaseOrderId aggregateId,
    IEnumerable<OrderLine> lines)
    : Command(aggregateId)
{
    public IEnumerable<OrderLine> Lines { get; } = lines;
}