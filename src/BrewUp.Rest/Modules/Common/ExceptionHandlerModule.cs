using BrewUp.Rest.Middleware;

namespace BrewUp.Rest.Modules.Common;

public sealed class ExceptionHandlerModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
