using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.Warehouse.Facade.Endpoints;

public static class WarehouseEndpoints
{
    public static IEndpointRouteBuilder MapWarehouseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/Warehouse")
            .WithTags("Warehouse");

        group.MapGet("/", () => Results.Ok(new { Message = "Warehouse module active", Version = "v1.0" }))
            .WithName("GetWarehouseStatus")
            .WithSummary("Get Warehouse module status");

        return app;
    }
}