using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.Facade;

public static class ModuleHelper
{
    public static IServiceCollection AddPurchaseFacade(this IServiceCollection services)
    {
        services.AddScoped<IPurchaseFacade, PurchaseFacade>();

        return services;
    }
}
