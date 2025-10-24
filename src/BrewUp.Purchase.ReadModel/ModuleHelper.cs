using BrewUp.Purchase.ReadModel.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Purchase.ReadModel;

public static class ModuleHelper
{
    public static IServiceCollection AddPurchaseReadModel(this IServiceCollection services)
    {
        services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();

        // Event handlers are registered in Infrastructure layer via RabbitMQ helper

        return services;
    }
}
