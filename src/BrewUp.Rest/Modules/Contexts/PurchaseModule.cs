using BrewUp.Purchase.Facade;

namespace BrewUp.Rest.Modules.Contexts;

public sealed class PurchaseModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 10;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddPurchaseFacade();

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        return app;
    }
}
