namespace BrewUp.Purchase.Facade.BindingModels.v1;

public sealed class PurchaseOrder
{
    public Guid Id { get; init; }
    public string OrderCode { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public decimal ReceivedQuantity { get; init; }
}
