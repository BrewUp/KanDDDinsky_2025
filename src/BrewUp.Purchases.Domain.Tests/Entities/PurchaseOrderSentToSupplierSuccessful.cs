using Brewup.Purchases.Domain.CommandHandlers;
using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Dtos;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using BrewUp.Purchases.SharedKernel.Messages.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Purchases.Domain.Tests.Entities;

public sealed class PurchaseOrderSentToSupplierSuccessful : CommandSpecification<SendPurchaseOrderToSupplier>
{
    private readonly PurchaseOrderId _purchaseOrderId;
    private readonly SupplierId _supplierId;

    private readonly OrderCreateDate _date;
    private readonly OrderDispatchDate _dispatchDate;

    private readonly IEnumerable<OrderLine> _lines;

    public PurchaseOrderSentToSupplierSuccessful()
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
    }

    protected override SendPurchaseOrderToSupplier When()
        => new (_purchaseOrderId, _dispatchDate);

    protected override ICommandHandlerAsync<SendPurchaseOrderToSupplier> OnHandler() =>
        new SendPurchaseOrderToSupplierHandlerAsync(Repository, new NullLoggerFactory());

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new PurchaseOrderSentToSupplier(_purchaseOrderId, _dispatchDate);
    }
}