using BrewUp.Purchase.SharedKernel.ValueObjects;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.Domain.Events;

public class PurchaseOrderCreated(PurchaseOrderId purchaseOrderId, BeerId beerId, BeerName beerName, Quantity quantity) : DomainEvent(purchaseOrderId)
{
    public PurchaseOrderId PurchaseOrderId { get; } = purchaseOrderId;
    public BeerId BeerId { get; } = beerId;
    public BeerName BeerName { get; } = beerName;
    public Quantity Quantity { get; } = quantity;
}