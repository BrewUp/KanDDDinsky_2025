using ZiggyCreatures.Caching.Fusion;

namespace BrewUp.Purchases.Rest.Modules;

public class ChacheModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        // Add base memory cache
        builder.Services.AddMemoryCache();
        
        // Configure and add FusionCache
        builder.Services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions {
                Duration = TimeSpan.FromMinutes(1),
        
                // FAIL-SAFE OPTIONS
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(30),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30)
            });

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app) => app;
}