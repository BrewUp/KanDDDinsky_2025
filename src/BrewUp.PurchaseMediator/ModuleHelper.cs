using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.PurchaseMediator;

public static class ModuleHelper
{
    public static IServiceCollection AddPurchaseMediator(this IServiceCollection services)
    {
        return services;
    }
}
