using INV.Domain.Entities.Purchases;
using INV.Domain.Shared;

namespace INV.App.Purchases
{
    public interface IPurchaseOrderService
    {
        ValueTask<Result<List<PurchaseOrder>>> GetPurchaseOrdersByDate(DateOnly dateOnly);

        ValueTask<Result<List<PurchaseOrderInfo>>> GetPurchaseOrderInfo();

        ValueTask<Result<List<PurchaseOrderInfo>>> GetPurchaseOrdersByIdSupplier(Guid idSupplier);

        ValueTask<Result<PurchaseOrder>> GetPurchaseOrdersById(Guid id);

        ValueTask<Result> ValicatePurchaseOrder(PurchaseOrder purchaseOrder);

        ValueTask<Result> CreatePurchaseOrder(PurchaseOrder purchaseOrder, List<PurchaseProduct> products);

        ValueTask<Result<List<PurchaseOrderInfo>>> GetPurchasesForReceiptCreation();

        ValueTask<Result<List<PurchaseProductInfo>>> GetProductsByPurchaseId(Guid purchaseId);

        ValueTask<Result> RemovePurchaseProduct(PurchaseProduct purchaseProduct);

        ValueTask<Result> DeleteAllPurchaseProduct(Guid purchaseOrderId);

        ValueTask<Result> UpdatePurchaseProduct(PurchaseProduct purchaseProduct);

        ValueTask<Result> UpdatePurchaseOrder(PurchaseOrder purchaseOrder);

        ValueTask<Result<PurchaseStatus>> GetPurchaseStatus(Guid id);
    }
}