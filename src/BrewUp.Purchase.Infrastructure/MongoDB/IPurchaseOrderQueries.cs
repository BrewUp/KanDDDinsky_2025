using BrewUp.Purchase.SharedKernel.DTOs;

namespace BrewUp.Purchase.Infrastructure.MongoDB;

public interface IPurchaseOrderQueries
{
    Task<PurchaseOrder?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken = default);
}
