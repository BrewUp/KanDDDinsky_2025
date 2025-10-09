using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.Purchase.Facade.Endpoints;

public static class PurchaseEndpoints
{
    public static IEndpointRouteBuilder MapPurchaseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/Purchase")
            .WithTags("Purchase");

        group.MapGet("/", () => Results.Ok(new { Message = "Purchase module active", Version = "v1.0" }))
            .WithName("GetPurchaseStatus")
            .WithSummary("Get Purchase module status");

        return app;
    }
}