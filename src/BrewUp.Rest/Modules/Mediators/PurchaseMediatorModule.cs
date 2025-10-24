using BrewUp.PurchaseMediator;
using BrewUp.Rest.Modules.Contexts;

namespace BrewUp.Rest.Modules.Mediators;

public sealed class PurchaseMediatorModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 50;
    public IEnumerable<IModule> DependsOn => [new PurchaseModule()];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddPurchaseMediator();
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapPurchaseOrders();
        return app;
    }
}
