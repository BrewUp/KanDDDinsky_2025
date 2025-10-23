using BrewUp.Purchase.SharedKernel.CustomTypes;
using BrewUp.Purchase.SharedKernel.Messages.Events;
using Muflone.Core;

namespace BrewUp.Purchase.Domain.Aggregates;

public class Order : AggregateRoot
{
    public PurchaseOrderId PurchaseOrderId { get; private set; } = null!;

    // Parameterless constructor for Muflone
    private Order() { }

    public Order(PurchaseOrderId purchaseOrderId, BeerId beerId, BeerName beerName, Quantity quantity)
    {
        PurchaseOrderId = purchaseOrderId;
        RaiseEvent(new PurchaseOrderCreated(purchaseOrderId, beerId, beerName, quantity));
    }

    public void ReceiveOrder()
    {
        RaiseEvent(new PurchaseOrderReceived(PurchaseOrderId));
    }

    public void ReceiveBeers(BeerId beerId, BeerName beerName, Quantity quantity)
    {
        RaiseEvent(new BeersReceived(beerId, beerName, quantity));
    }

    public void LoadBeerInStock(BeerId beerId, BeerName beerName, Quantity quantity)
    {
        RaiseEvent(new BeerLoadedInStock(beerId, beerName, quantity));
    }
}