using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.SharedKernel;

public static class PurchaseSharedKernelHelper
{
    public static IServiceCollection AddPurchaseSharedKernel(this IServiceCollection services)
    {
        return services;
    }
}