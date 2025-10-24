using BrewUp.Purchase.Domain.Commands.Handlers;
using BrewUp.Purchase.ReadModel.Events.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Muflone;

namespace BrewUp.Purchase.Infrastructure.RabbitMQ;

public static class PurchaseRabbitMqHelper
{
    public static IServiceCollection AddPurchaseMessageHandlers(this IServiceCollection services)
    {
        // Command Handlers
        services.AddCommandHandler<CreatePurchaseOrderHandler>();
        services.AddCommandHandler<AcknowledgeReceivingOrderHandler>();

        // Event Handlers
        services.AddDomainEventHandler<PurchaseOrderCreatedHandler>();
        services.AddDomainEventHandler<PurchaseOrderReceivedHandler>();
        services.AddDomainEventHandler<PurchaseOrderStatusChangedHandler>();

        return services;
    }
}
