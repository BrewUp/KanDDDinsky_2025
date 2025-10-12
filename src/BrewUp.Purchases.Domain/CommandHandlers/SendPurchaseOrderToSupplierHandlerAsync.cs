using BrewUp.Purchases.Domain.Entities;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public sealed class SendPurchaseOrderToSupplierHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<SendPurchaseOrderToSupplier>(repository, loggerFactory)
{
    public override async Task ProcessCommand(SendPurchaseOrderToSupplier command, CancellationToken cancellationToken = default)
    {
        var aggregate  = await Repository.GetByIdAsync<Order>(command.AggregateId, cancellationToken);
        aggregate!.SendOrderToSupplier(command.DispatchDate);

        Logger.LogInformation($"Order {aggregate.Id.Value} sent to supplier");
        await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }
}