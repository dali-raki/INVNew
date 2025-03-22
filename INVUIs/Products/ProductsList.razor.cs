using INV.App.Products;
using INV.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;
using INVUIs.Products.ProductsModel;
using Radzen;
using Radzen.Blazor;
using INVUIs.Purchases.PurchaseModels;

namespace INVUIs.Products
{
    public partial class ProductsList
    {
        [Parameter] public EventCallback<PurchaseProductModel> OnCommand { get; set; }
        [Inject] public NavigationManager navigationManager { set; get; }
        [Inject] private IProductService ProductService { get; set; }
        [Parameter] public List<ProductInfo> Products { get; set; }
        public ProductForm productForm;
        public ProductDetail productEdit;
        public ProductEditForm productEditForm;
        private RadzenDataGrid<ProductInfo> grid;

        public async Task navigatepage(Guid id) => Navigation.NavigateTo($"/products/{id}");

        private bool CommandSelected = false;

        private PurchaseProductModel newProduct = new PurchaseProductModel();
        private bool showForm = false;

        private void EditProduct(ProductDetail product)
        {
            productEdit = new ProductDetail
            {
                Designation = product.Designation,
                TVA = product.TVA,
                UnitMeasure = product.UnitMeasure,
                WareHouse = product.WareHouse,
            };

            productForm.ShowModal();
        }

        private async Task DeleteProduct(Guid productId)
        {
            var productToRemove = Products.Find(p => p.Id == productId);

            Products.Remove(productToRemove);
            var result = await ProductService.RemoveProduct(productId);
            await grid.Reload();

            StateHasChanged();
        }

        private bool isLoading = false;

        private async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }
    }
}