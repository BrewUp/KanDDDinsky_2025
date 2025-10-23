using BrewUp.Purchase.Domain;
using BrewUp.Purchase.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.Facade;

public static class PurchaseFacadeHelper
{
    public static IServiceCollection AddPurchaseFacade(this IServiceCollection services, string connectionString = "")
    {
        // Register any services related to the Purchase facade here
        services.AddScoped<IPurchaseFacade, PurchaseFacadeService>();

        services.AddPurchaseDomain();
        services.AddPurchaseInfrastructure(connectionString);

        return services;
    }
}

internal class PurchaseFacadeService : IPurchaseFacade
{
    // Implementation will be added here
}