using BrewUp.Purchase.SharedKernel.ValueObjects;
using Muflone.Messages.Commands;

namespace BrewUp.Purchase.Domain.Commands;

public class CreatePurchaseOrder(PurchaseOrderId purchaseOrderId, BeerId beerId, BeerName beerName, Quantity quantity) : Command(purchaseOrderId)
{
    public PurchaseOrderId PurchaseOrderId { get; } = purchaseOrderId;
    public BeerId BeerId { get; } = beerId;
    public BeerName BeerName { get; } = beerName;
    public Quantity Quantity { get; } = quantity;
}