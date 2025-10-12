using BrewUp.Purchases.Domain.Entities;
using BrewUp.Purchases.SharedKernel.CustomTypes;
using BrewUp.Purchases.SharedKernel.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Purchases.Domain.CommandHandlers;

public class CreatePurchaseOrderHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
	: CommandHandlerBaseAsync<CreatePurchaseOrder>(repository, loggerFactory)
{
	public override async Task ProcessCommand(CreatePurchaseOrder command, CancellationToken cancellationToken = default)
	{
		var aggregate = Order.Create(new PurchaseOrderId(command.AggregateId.Value), command.SupplierId, command.Date, command.Lines);
		Logger.LogInformation($"Order created aggregateId: {aggregate.Id}");
		await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
	}
}