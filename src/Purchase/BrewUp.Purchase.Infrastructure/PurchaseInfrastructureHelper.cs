using Microsoft.Extensions.DependencyInjection;
using BrewUp.Purchase.Domain.CommandHandlers;
using BrewUp.Purchase.ReadModel.EventHandlers;

namespace BrewUp.Purchase.Infrastructure;

public static class PurchaseInfrastructureHelper
{
    public static IServiceCollection AddPurchaseInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Register command handlers
        services.AddScoped<CreatePurchaseOrderHandler>();
        services.AddScoped<LoadBeerInStockHandler>();
        
        // Register event handlers
        services.AddScoped<PurchaseOrderCreatedHandler>();
        services.AddScoped<PurchaseOrderReceivedHandler>();
        services.AddScoped<BeersReceivedHandler>();
        services.AddScoped<BeerLoadedInStockHandler>();
        
        return services;
    }
}

public class EventStoreSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}