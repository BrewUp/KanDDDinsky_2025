using BrewUp.Purchase.Domain;
using BrewUp.Purchase.Infrastructure.EventStore;
using BrewUp.Purchase.Infrastructure.MongoDB;
using BrewUp.Purchase.Infrastructure.RabbitMQ;
using BrewUp.Purchase.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore.gRPC;

namespace BrewUp.Purchase.Infrastructure;

public static class ModuleHelper
{
    public static IServiceCollection AddPurchaseInfrastructure(
        this IServiceCollection services,
        MongoDbSettings mongoDbSettings,
        EventStoreSettings eventStoreSettings,
        RabbitMqSettings rabbitMqSettings)
    {
        // Register MongoDB
        services.AddPurchaseMongoDb(mongoDbSettings);

        // Register Domain and ReadModel layers
        services.AddPurchaseDomain();
        services.AddPurchaseReadModel();

        // Register EventStore
        services.AddMufloneEventStore(eventStoreSettings.ConnectionString);

        // Register RabbitMQ (includes message handlers)
        services.AddPurchaseRabbitMq(rabbitMqSettings);

        return services;
    }
}
