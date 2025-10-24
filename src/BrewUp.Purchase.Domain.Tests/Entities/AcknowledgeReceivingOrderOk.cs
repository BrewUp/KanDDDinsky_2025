using BrewUp.Messages.Commands;
using BrewUp.Messages.Events;
using BrewUp.Purchase.Domain.Commands.Handlers;
using BrewUp.Purchase.SharedKernel.DomainIds;
using BrewUp.Purchase.SharedKernel.Enumerations;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.CustomTypes;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Purchase.Domain.Tests.Entities;

public sealed class AcknowledgeReceivingOrderOk : CommandSpecification<AcknowledgeReceivingOrder>
{
    private readonly PurchaseOrderId _purchaseOrderId = new(Guid.NewGuid());
    private readonly Account _user = new(Guid.NewGuid().ToString(), "test@example.com");
    private const string OrderCode = "PO-2025-001";
    private const decimal ReceivedQuantity = 100.5m;

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new PurchaseOrderCreated(_purchaseOrderId, OrderCode);
    }

    protected override AcknowledgeReceivingOrder When()
    {
        return new AcknowledgeReceivingOrder(_purchaseOrderId, ReceivedQuantity, _user);
    }

    protected override ICommandHandlerAsync<AcknowledgeReceivingOrder> OnHandler()
    {
        return new AcknowledgeReceivingOrderHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new PurchaseOrderReceived(_purchaseOrderId, ReceivedQuantity);
        yield return new PurchaseOrderStatusChanged(_purchaseOrderId, PurchaseOrderStatus.Complete);
    }
}
