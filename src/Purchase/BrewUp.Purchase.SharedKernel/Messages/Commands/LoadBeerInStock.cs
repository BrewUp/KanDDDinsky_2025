using BrewUp.Purchase.SharedKernel.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Purchase.SharedKernel.Messages.Commands;

public class LoadBeerInStock(BeerId beerId, BeerName beerName, Quantity quantity) : Command(beerId)
{
    public BeerId BeerId { get; } = beerId;
    public BeerName BeerName { get; } = beerName;
    public Quantity Quantity { get; } = quantity;
}