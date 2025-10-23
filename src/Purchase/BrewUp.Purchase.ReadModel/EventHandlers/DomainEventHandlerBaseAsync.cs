namespace BrewUp.Purchase.ReadModel.EventHandlers;

public abstract class DomainEventHandlerBaseAsync<TEvent> : IDisposable
{
    public abstract Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}