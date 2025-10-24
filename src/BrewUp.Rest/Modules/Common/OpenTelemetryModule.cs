using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

namespace BrewUp.Rest.Modules.Common;

public sealed class OpenTelemetryOptions
{
    public string ServiceName { get; init; } = "brewup";
    public string ServiceVersion { get; init; } = "1.0.0";
    public string ServiceInstanceId { get; init; } = "1";
    public string ExporterEndpoint { get; init; } = "http://localhost:4317/";
    public double TraceRatioSampling { get; init; } = 1.0;
}

public sealed class OpenTelemetryModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 1;
    public IEnumerable<IModule> DependsOn => [];

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetSection("OpenTelemetry")
            .Get<OpenTelemetryOptions>() ?? new OpenTelemetryOptions();

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(
                    serviceName: config.ServiceName,
                    serviceVersion: config.ServiceVersion,
                    serviceInstanceId: config.ServiceInstanceId,
                    autoGenerateServiceInstanceId: false))
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                metrics.AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = httpContext =>
                        !httpContext.Request.Path.StartsWithSegments("/health") &&
                        !httpContext.Request.Path.StartsWithSegments("/openapi") &&
                        !httpContext.Request.Path.StartsWithSegments("/scalar");
                });

                tracing.AddHttpClientInstrumentation();
                tracing.SetSampler(new TraceIdRatioBasedSampler(config.TraceRatioSampling));
            });

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        return app;
    }
}
