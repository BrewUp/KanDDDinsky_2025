using BrewUp.Purchases.Rest.Helpers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BrewUp.Purchases.Rest.Modules;

public sealed class StatusModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();

        return builder.Services;
    }

    WebApplication IModule.Configure(WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckExtensions.WriteResponse
        });

        return app;
    }
}