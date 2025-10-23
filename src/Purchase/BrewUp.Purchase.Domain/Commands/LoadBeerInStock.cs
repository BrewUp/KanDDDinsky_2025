using BrewUp.Purchase.SharedKernel.ValueObjects;
using Muflone.Messages.Commands;

namespace BrewUp.Purchase.Domain.Commands;

public class LoadBeerInStock(BeerId beerId, BeerName beerName, Quantity quantity) : Command(beerId)
{
    public BeerId BeerId { get; } = beerId;
    public BeerName BeerName { get; } = beerName;
    public Quantity Quantity { get; } = quantity;
}