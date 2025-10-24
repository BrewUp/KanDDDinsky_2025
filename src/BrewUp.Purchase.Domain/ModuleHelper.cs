using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.Domain;

public static class ModuleHelper
{
    public static IServiceCollection AddPurchaseDomain(this IServiceCollection services)
    {
        // Domain services registration (if any)
        // Command handlers are registered in Infrastructure layer via RabbitMQ helper

        return services;
    }
}
