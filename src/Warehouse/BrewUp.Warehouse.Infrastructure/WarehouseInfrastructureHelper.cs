using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouse.Infrastructure;

public static class WarehouseInfrastructureHelper
{
    public static IServiceCollection AddWarehouseInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure services here
        
        return services;
    }
}

public class EventStoreSettings
{
    public string ConnectionString { get; set; } = string.Empty;
}