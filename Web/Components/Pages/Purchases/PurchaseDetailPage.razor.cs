using INV.App.Purchases;
using INV.App.Receipts;
using INV.Domain.Entities.Products;
using INV.Domain.Entities.Purchases;
using INV.Domain.Entities.Receipts;
using INV.Domain.Shared;
using INV.Implementation.Service.Purchses;
using INVUIs.Products.ProductsModel;
using Microsoft.AspNetCore.Components;

namespace INV.Web.Components.Pages.Purchases
{
    public partial class PurchaseDetailPage
    {
        [Parameter] public Guid Id { get; set; }
        [Inject] public IPurchaseOrderService purchaseOrderService { set; get; }
        [Inject] public IReceiptService receiptService { set; get; }

        public PurchaseOrder purchaseOrder = new PurchaseOrder();

        public List<PurchaseProductInfo> products;

        public List<Receipt> receptionsListByPurchase;

        protected override async Task OnInitializedAsync()
        {
            var resultToPurchase = await purchaseOrderService.GetPurchaseOrdersById(Id);
            if (resultToPurchase.IsSuccess)
            {
                purchaseOrder = resultToPurchase.Value;
            }
            var resultToproduct = await purchaseOrderService.GetProductsByPurchaseId(Id);
            if (resultToproduct.IsSuccess)
            {
                products = resultToproduct.Value;
            }
            var receiptsByPurchase=await receiptService.GetReceiptsByPurchaseIdWhenStatus(purchaseOrder.Id);
            if (receiptsByPurchase.IsSuccess)
            {
                receptionsListByPurchase = receiptsByPurchase.Value.ToList();
            }
        }
    }
}