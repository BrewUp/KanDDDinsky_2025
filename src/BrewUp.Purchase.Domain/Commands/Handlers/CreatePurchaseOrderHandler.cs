using BrewUp.Messages.Commands;
using BrewUp.Purchase.SharedKernel.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace BrewUp.Purchase.Domain.Commands.Handlers;

public sealed class CreatePurchaseOrderHandler(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerAsync<CreatePurchaseOrder>(repository, loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreatePurchaseOrderHandler>();

    public override async Task HandleAsync(CreatePurchaseOrder command, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processing command: CreatePurchaseOrder - AggregateId: {AggregateId}, OrderCode: {OrderCode}",
            command.AggregateId, command.OrderCode);

        var purchaseOrder = Entities.PurchaseOrder.CreatePurchaseOrder(
            (PurchaseOrderId)command.AggregateId,
            command.OrderCode);

        await Repository.SaveAsync(purchaseOrder, Guid.NewGuid(), cancellationToken);

        _logger.LogInformation("Purchase order created successfully: {AggregateId}", command.AggregateId);
    }
}
