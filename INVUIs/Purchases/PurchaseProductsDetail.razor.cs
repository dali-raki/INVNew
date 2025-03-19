using INV.App.Purchases;
using INV.Domain.Entities.Purchases;
using INVUIs.Products;
using INVUIs.Products.ProductsModel;
using INVUIs.Shared;
using Microsoft.AspNetCore.Components;

namespace INVUIs.Purchases;

public partial class PurchaseProductsDetail
{
    [Parameter] public List<ProductModel> products { get; set; }
    [Inject] public IPurchaseOrderService purchaseOrderService { get; set; }
    private bool display = false;
    private PurchaseProductInfo product = new PurchaseProductInfo();
    private PurchaseProduct editproduct;
    private ProductEditForm productEditForm;
    private ConformationForm conformationForm;
    private PurchaseStatus PurchaseStatus;
    private Guid PurchaseId;

    /*  protected override async void OnAfterRender(bool firstRender)

      {
          if (products != null && products.Count > 0)
          {
              PurchaseId = products.FirstOrDefault().PurchaseId;
              var result = await purchaseOrderService.GetPurchaseStatus(PurchaseId);
              if (result.IsSuccess)
              {
                  PurchaseStatus = result.Value;
              }

              StateHasChanged();
          }
      }

      public async void EditProduct(PurchaseProductInfo product)
      {
          this.product = product;
          productEditForm.show();
      }

      public void DeleteProduct()
      {
          conformationForm.show();
      }

      private async Task OnConfirmDelete()
      {
          await purchaseOrderService.RemovePurchaseProduct(new PurchaseProduct
          {
              PurchaseOrderId = product.PurchaseId,
              ProductId = product.ProductId
          });

          // Refresh the product list
          var result = await purchaseOrderService.GetProductsByPurchaseId(product.PurchaseId);
          if (result.IsSuccess)
          {
              products = result.Value;
          }

          StateHasChanged();
      }

      public void showAddProduct()
      {
      }*/
}