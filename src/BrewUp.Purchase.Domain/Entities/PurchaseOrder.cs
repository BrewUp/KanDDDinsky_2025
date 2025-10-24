using BrewUp.Messages.Events;
using BrewUp.Purchase.SharedKernel.DomainIds;
using BrewUp.Purchase.SharedKernel.Enumerations;
using Muflone.Core;

namespace BrewUp.Purchase.Domain.Entities;

public sealed class PurchaseOrder : AggregateRoot
{
    private PurchaseOrderStatus _status;
    private string _orderCode = string.Empty;
    private decimal _receivedQuantity;

    private PurchaseOrder()
    {
    }

    private PurchaseOrder(PurchaseOrderId aggregateId, string orderCode)
    {
        if (string.IsNullOrWhiteSpace(orderCode))
            throw new ArgumentException("Order code cannot be empty", nameof(orderCode));

        RaiseEvent(new PurchaseOrderCreated(aggregateId, orderCode));
    }

    public static PurchaseOrder CreatePurchaseOrder(PurchaseOrderId aggregateId, string orderCode)
    {
        return new PurchaseOrder(aggregateId, orderCode);
    }

    public void AcknowledgeReceiving(decimal receivedQuantity)
    {
        if (receivedQuantity <= 0)
            throw new ArgumentException("Received quantity must be greater than zero", nameof(receivedQuantity));

        if (_status != PurchaseOrderStatus.Created)
            throw new InvalidOperationException($"Cannot acknowledge receiving for order in status {_status}");

        RaiseEvent(new PurchaseOrderReceived((PurchaseOrderId)Id, receivedQuantity));
        RaiseEvent(new PurchaseOrderStatusChanged((PurchaseOrderId)Id, PurchaseOrderStatus.Complete));
    }

    private void Apply(PurchaseOrderCreated @event)
    {
        Id = @event.AggregateId;
        _orderCode = @event.OrderCode;
        _status = PurchaseOrderStatus.Created;
    }

    private void Apply(PurchaseOrderReceived @event)
    {
        _receivedQuantity = @event.ReceivedQuantity;
        _status = PurchaseOrderStatus.Received;
    }

    private void Apply(PurchaseOrderStatusChanged @event)
    {
        _status = @event.NewStatus;
    }
}
