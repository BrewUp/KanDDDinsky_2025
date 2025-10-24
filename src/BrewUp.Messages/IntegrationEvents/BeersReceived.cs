using Muflone.Messages.Events;

namespace BrewUp.Messages.IntegrationEvents;

public sealed class BeersReceived(Guid orderId, decimal quantity, DateTime receivedAt)
    : IntegrationEvent(new IntegrationEventId(Guid.NewGuid()))
{
    public Guid OrderId { get; } = orderId;
    public decimal Quantity { get; } = quantity;
    public DateTime ReceivedAt { get; } = receivedAt;
}
