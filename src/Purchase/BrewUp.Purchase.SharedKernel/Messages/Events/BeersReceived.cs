using BrewUp.Purchase.SharedKernel.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Purchase.SharedKernel.Messages.Events;

public class BeersReceived(BeerId beerId, BeerName beerName, Quantity quantity) : DomainEvent(beerId)
{
    public BeerId BeerId { get; } = beerId;
    public BeerName BeerName { get; } = beerName;
    public Quantity Quantity { get; } = quantity;
}