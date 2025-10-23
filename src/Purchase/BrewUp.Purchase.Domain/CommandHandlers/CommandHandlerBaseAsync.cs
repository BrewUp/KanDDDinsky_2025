using Muflone.Messages.Commands;

namespace BrewUp.Purchase.Domain.CommandHandlers;

public abstract class CommandHandlerBaseAsync<TCommand> where TCommand : Command
{
    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}