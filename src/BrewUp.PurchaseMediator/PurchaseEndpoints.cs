using BrewUp.Purchase.Facade;
using BrewUp.Purchase.Facade.BindingModels.v1.Input;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.PurchaseMediator;

public static class PurchaseEndpoints
{
    public static void MapPurchaseOrders(this WebApplication app)
    {
        var group = app.MapGroup("/v1/purchase-orders")
            .WithName("PurchaseOrders")
            .WithOpenApi()
            .WithTags("Purchase");

        group.MapPost("/", CreatePurchaseOrder)
            .WithName("CreatePurchaseOrder")
            .WithSummary("Create a new purchase order")
            .WithOpenApi();

        group.MapPost("/{orderId:guid}/acknowledge", AcknowledgeReceiving)
            .WithName("AcknowledgeReceiving")
            .WithSummary("Acknowledge receiving order from supplier")
            .WithOpenApi();

        group.MapGet("/{orderId:guid}", GetPurchaseOrder)
            .WithName("GetPurchaseOrder")
            .WithSummary("Get purchase order by ID")
            .WithOpenApi();
    }

    private static async Task<IResult> CreatePurchaseOrder(
        CreatePurchaseOrderRequest request,
        IPurchaseFacade facade,
        CancellationToken cancellationToken)
    {
        var purchaseOrder = await facade.CreatePurchaseOrderAsync(request, cancellationToken);
        return Results.Created($"/v1/purchase-orders/{purchaseOrder.Id}", purchaseOrder);
    }

    private static async Task<IResult> AcknowledgeReceiving(
        Guid orderId,
        IPurchaseFacade facade,
        CancellationToken cancellationToken)
    {
        var request = new AcknowledgeReceivingRequest { OrderId = orderId };
        await facade.AcknowledgeReceivingAsync(request, cancellationToken);
        return Results.NoContent();
    }

    private static async Task<IResult> GetPurchaseOrder(
        Guid orderId,
        IPurchaseFacade facade,
        CancellationToken cancellationToken)
    {
        var purchaseOrder = await facade.GetPurchaseOrderByIdAsync(orderId, cancellationToken);

        if (purchaseOrder == null)
            return Results.NotFound();

        return Results.Ok(purchaseOrder);
    }
}
