using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.Infrastructure;

public static class PurchaseInfrastructureHelper
{
    public static IServiceCollection AddPurchaseInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure services here
        
        return services;
    }
}

public class EventStoreSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}