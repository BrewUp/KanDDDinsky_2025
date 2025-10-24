using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BrewUp.Purchase.Infrastructure.MongoDB;

public static class MongoDbHelper
{
    public static IServiceCollection AddPurchaseMongoDb(
        this IServiceCollection services,
        MongoDbSettings mongoDbSettings)
    {
        // Register MongoDB client
        services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoDbSettings.ConnectionString));

        // Register database
        services.AddScoped(serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoDbSettings.DatabaseName);
        });

        // Register queries
        services.AddScoped<IPurchaseOrderQueries, PurchaseOrderQueries>();

        return services;
    }
}
