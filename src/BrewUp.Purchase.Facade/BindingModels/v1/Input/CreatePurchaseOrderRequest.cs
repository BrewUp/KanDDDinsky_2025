namespace BrewUp.Purchase.Facade.BindingModels.v1.Input;

public sealed class CreatePurchaseOrderRequest
{
    public string OrderCode { get; init; } = string.Empty;
}
