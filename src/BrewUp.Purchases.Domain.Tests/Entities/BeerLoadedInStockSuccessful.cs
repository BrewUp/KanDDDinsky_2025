using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using BrewUp.Purchases.SharedKernel.Messages.Events;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;
using OrderLine = BrewUp.Purchases.SharedKernel.Dtos.OrderLine;
using Price = BrewUp.Purchases.SharedKernel.Dtos.Price;
using Quantity = BrewUp.Purchases.SharedKernel.Dtos.Quantity;

namespace Brewup.Purchases.Domain.Tests.Entities;

public sealed class BeerLoadedInStockSuccessful : CommandSpecification<LoadBeerInStock>
{
    private readonly PurchaseOrderId _purchaseOrderId;
    private readonly SupplierId _supplierId;

    private readonly OrderCreateDate _date;
    private readonly OrderDispatchDate _dispatchDate;

    private readonly IEnumerable<OrderLine> _lines;
    
    public BeerLoadedInStockSuccessful()
    {
        _purchaseOrderId = new PurchaseOrderId(Guid.NewGuid().ToString());
        _supplierId = new SupplierId(Guid.NewGuid().ToString());
        _date = new OrderCreateDate(DateTime.Today);
        _dispatchDate = new OrderDispatchDate(DateTime.Today.AddDays(2));

        _lines = [];
        _lines = _lines.Concat(new List<OrderLine>
        {
            new()
            {
                BeerId = new BeerId(Guid.NewGuid().ToString()),
                BeerName = new BeerName("Product 1"),
                Quantity = new Quantity {UnitOfMeasure = "N.", Value = 1},
                Price = new Price {Currency = "EUR", Value = 1}
            },
            new()
            {
                BeerId = new BeerId(Guid.NewGuid().ToString()),
                BeerName = new BeerName("Product 2"),
                Quantity = new Quantity {UnitOfMeasure = "N.", Value = 2},
                Price = new Price {Currency = "EUR", Value = 2}
            }
        });
    }
    
    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new PurchaseOrderCreated(_purchaseOrderId, _supplierId, _date, _lines);
        yield return new PurchaseOrderSentToSupplier(_purchaseOrderId, _dispatchDate);
        yield return new PurchaseOrderStatusChangedToComplete(_purchaseOrderId, _lines);
    }

    protected override LoadBeerInStock When() => 
        new (_purchaseOrderId, _lines);

    protected override ICommandHandlerAsync<LoadBeerInStock> OnHandler()
    {
        throw new NotImplementedException();
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        throw new NotImplementedException();
    }
}