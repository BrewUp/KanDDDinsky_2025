using BrewUp.Purchase.SharedKernel.DTOs;
using BrewUp.Purchase.SharedKernel.Enumerations;

namespace BrewUp.Purchase.ReadModel.Services;

public interface IPurchaseOrderService
{
    Task CreatePurchaseOrderAsync(Guid orderId, string orderCode, PurchaseOrderStatus status, DateTime createdAt, CancellationToken cancellationToken = default);

    Task UpdateReceivedQuantityAsync(Guid orderId, decimal receivedQuantity, DateTime updatedAt, CancellationToken cancellationToken = default);

    Task UpdateStatusAsync(Guid orderId, PurchaseOrderStatus newStatus, DateTime updatedAt, CancellationToken cancellationToken = default);

    Task<PurchaseOrder?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
}
