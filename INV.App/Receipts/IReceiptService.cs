using INV.Domain.Entities.Receipts;
using INV.Domain.Shared;
namespace INV.App.Receipts
{
    public interface IReceiptService
    {
        ValueTask<Result<ReceiptInfo>> CreateReceiptFromPurchase(Guid purchaseId);

        ValueTask<Result> ValidateReceipt(Guid receiptId);

        ValueTask<Result<List<ReceiptInfo>>> GetAllReceipts();

        ValueTask<Result<Receipt?>> GetReceiptById(Guid id);

        ValueTask<Result<List<Receipt>>> GetReceiptsByPurchaseId(Guid purchaseId);

        ValueTask<Result> CreateReceipt(Receipt receipt);

        ValueTask<Result> UpdateReceipt(Receipt receipt);

        ValueTask<Result> RemoveReceipt(Guid id);

        ValueTask<Result<List<ReceiptProduct>>> GetAllReceiptProducts();

        ValueTask<Result<List<ReceiptProduct>>> GetProductsByReceptionId(Guid receptionId);

        ValueTask<Result> RemoveReceiptProductAsync(Guid receptionId, Guid productId);

        ValueTask<Result<ReceiptInfo>> GetReceiptInfoById(Guid receiptId);

        ValueTask<Result<List<ReceiptInfo>>> GetReceiptsBySupplierId(Guid supplierId);

        ValueTask<Result<List<Receipt>>> GetReceiptsByPurchaseIdWhenStatus(Guid purchaseId);

        ValueTask<Result<bool>> ReceiptExistById(Guid id);
    }
}