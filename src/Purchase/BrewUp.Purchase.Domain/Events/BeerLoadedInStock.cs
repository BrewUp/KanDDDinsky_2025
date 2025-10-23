using BrewUp.Purchase.SharedKernel.ValueObjects;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.Domain.Events;

public class BeerLoadedInStock(BeerId beerId, BeerName beerName, Quantity quantity) : DomainEvent(beerId)
{
    public BeerId BeerId { get; } = beerId;
    public BeerName BeerName { get; } = beerName;
    public Quantity Quantity { get; } = quantity;
}