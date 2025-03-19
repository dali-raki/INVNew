using INV.App.Products;
using INV.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;
using INVUIs.Products.ProductsModel;
using Radzen;
using Radzen.Blazor;

namespace INVUIs.Products
{
    public partial class ProductsList
    {
        [Parameter] public EventCallback<ProductModel> OnCommand { get; set; }
        [Inject] public NavigationManager navigationManager { set; get; }
        [Inject] private IProductService ProductService { get; set; }
        [Parameter] public List<Product> Products { get; set; }
        public ProductForm productForm;
        public ProductEditForm productEditForm;
        private RadzenDataGrid<Product> grid;

        public async Task navigatepage(Guid id) => Navigation.NavigateTo($"/products/{id}");

        private bool CommandSelected = false;

        private ProductModel newProduct = new ProductModel();
        private bool showForm = false;

        private void EditProduct(Product product)
        {
            newProduct = new ProductModel
            {
                ID = product.Id,
                Designation = product.Designation,
                Quantity = product.Quantity,
                TVA = product.TVA,
                UnitMeasure = product.UnitMeasure,
                UnitPrice = product.UnitPrice,
                DeliveryTime = 1,
                WareHouse = "WareHouse",
                TotalPrice = product.UnitPrice * product.Quantity
            };

            productEditForm.show();
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