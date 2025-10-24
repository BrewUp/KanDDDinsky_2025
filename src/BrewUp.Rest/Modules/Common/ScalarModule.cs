using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace BrewUp.Rest.Modules.Common;

public sealed class ScalarModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Servers = [new OpenApiServer { Url = "/" }];
                document.Info = new OpenApiInfo
                {
                    Title = "BrewUp Purchase API",
                    Version = "v1.0",
                    Description = "API for Purchase bounded context in BrewUp ERP",
                    Contact = new OpenApiContact
                    {
                        Name = "BrewUp Team"
                    }
                };

                return Task.CompletedTask;
            });
        });

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("BrewUp Purchase API v1.0")
                    .WithTheme(ScalarTheme.DeepSpace);
            });
        }

        return app;
    }
}
