using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace BrewUp.Rest.Modules.Common;

public sealed class HealthModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 50;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
