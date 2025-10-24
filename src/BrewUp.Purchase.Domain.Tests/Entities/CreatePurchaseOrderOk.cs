using BrewUp.Messages.Commands;
using BrewUp.Messages.Events;
using BrewUp.Purchase.Domain.Commands.Handlers;
using BrewUp.Purchase.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.CustomTypes;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Purchase.Domain.Tests.Entities;

public sealed class CreatePurchaseOrderOk : CommandSpecification<CreatePurchaseOrder>
{
    private readonly PurchaseOrderId _purchaseOrderId = new(Guid.NewGuid());
    private readonly Account _user = new(Guid.NewGuid().ToString(), "test@example.com");
    private const string OrderCode = "PO-2025-001";

    protected override IEnumerable<DomainEvent> Given()
    {
        yield break;
    }

    protected override CreatePurchaseOrder When()
    {
        return new CreatePurchaseOrder(_purchaseOrderId, OrderCode, _user);
    }

    protected override ICommandHandlerAsync<CreatePurchaseOrder> OnHandler()
    {
        return new CreatePurchaseOrderHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new PurchaseOrderCreated(_purchaseOrderId, OrderCode);
    }
}
