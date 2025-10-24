using BrewUp.Purchase.Facade.BindingModels.v1;
using BrewUp.Purchase.Facade.BindingModels.v1.Input;

namespace BrewUp.Purchase.Facade;

public interface IPurchaseFacade
{
    Task<PurchaseOrder> CreatePurchaseOrderAsync(CreatePurchaseOrderRequest request, CancellationToken cancellationToken = default);
    Task AcknowledgeReceivingAsync(AcknowledgeReceivingRequest request, CancellationToken cancellationToken = default);
    Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
}
