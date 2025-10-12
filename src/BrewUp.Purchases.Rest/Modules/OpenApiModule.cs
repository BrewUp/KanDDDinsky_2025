using Microsoft.OpenApi.Models;

namespace BrewUp.Purchases.Rest.Modules;

public class OpenApiModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Servers = [new OpenApiServer {Url = "/"}];
                document.Info = new OpenApiInfo
                {
                    Title = "Muflone API",
                    Version = "v1.0",
                    Description = "Muflone API",
                    Contact = new OpenApiContact
                    {
                        Name = "Muflone"
                    }
                };

                return Task.CompletedTask;
            });
        });

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapOpenApi();
        // app.MapScalarApiReference(options =>
        // {
        //     options.WithTitle("Muflone API")
        //         .WithTheme(ScalarTheme.None);
        // });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Beers LLM API v1.0");
        });

        return app;
    }
}