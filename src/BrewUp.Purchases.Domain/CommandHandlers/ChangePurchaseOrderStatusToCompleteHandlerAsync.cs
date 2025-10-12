using BrewUp.Purchases.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public class ChangePurchaseOrderStatusToCompleteHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<ChangePurchaseOrderStatusToComplete>(repository, loggerFactory)
{
    public override Task ProcessCommand(ChangePurchaseOrderStatusToComplete command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}