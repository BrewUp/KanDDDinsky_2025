using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.Domain;

public static class PurchaseDomainHelper
{
    public static IServiceCollection AddPurchaseDomain(this IServiceCollection services)
    {
        // Register domain services here
        
        return services;
    }
}