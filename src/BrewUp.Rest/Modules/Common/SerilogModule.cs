using Serilog;

namespace BrewUp.Rest.Modules.Common;

public sealed class SerilogModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        return app;
    }
}
