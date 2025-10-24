using BrewUp.Purchase.Facade;
using BrewUp.Purchase.Infrastructure;
using BrewUp.Purchase.Infrastructure.EventStore;
using BrewUp.Purchase.Infrastructure.MongoDB;
using BrewUp.Purchase.Infrastructure.RabbitMQ;

namespace BrewUp.Rest.Modules.Contexts;

public sealed class PurchaseModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 10;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        // Register facade layer
        builder.Services.AddPurchaseFacade();

        // Read configuration settings
        var mongoDbSettings = builder.Configuration.GetSection("Purchase:MongoDB").Get<MongoDbSettings>()
            ?? throw new ArgumentException("MongoDB settings not found");
        var eventStoreSettings = builder.Configuration.GetSection("Purchase:EventStore").Get<EventStoreSettings>()
            ?? throw new ArgumentException("EventStore settings not found");
        var rabbitMqSettings = builder.Configuration.GetSection("Purchase:RabbitMQ").Get<RabbitMqSettings>()
            ?? throw new ArgumentException("RabbitMQ settings not found");

        // Register infrastructure layer (includes Domain, ReadModel, MongoDB, EventStore, RabbitMQ)
        builder.Services.AddPurchaseInfrastructure(
            mongoDbSettings,
            eventStoreSettings,
            rabbitMqSettings);

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        // Initialization logic if needed (migrations, etc.)
        return app;
    }
}
