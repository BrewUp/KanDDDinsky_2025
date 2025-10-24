using BrewUp.Purchase.SharedKernel.Enumerations;

namespace BrewUp.Purchase.SharedKernel.DTOs;

public sealed class PurchaseOrder
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public PurchaseOrderStatus Status { get; set; }
    public decimal ReceivedQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
