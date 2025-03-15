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
        [Inject] IProductService ProductService { get; set; }
        [Parameter] public List<Product> Products { get; set; }
        public ProductForm productForm;
        private RadzenDataGrid<Product> grid;
        public async Task navigatepage(Guid id) => Navigation.NavigateTo($"/productDetail/{id}");


        private bool CommandSelected = false;

        private ProductModel newProduct = new ProductModel();
        private bool showForm = false;


        private void EditProduct(Product product)
        {
        }

        private async Task DeleteProduct(Guid productId)
        {
           var productToRemove = Products.Find(p => p.Id == productId);

            Products.Remove(productToRemove);
            var result=await ProductService.RemoveProduct(productId);
            await grid.Reload();
           
            StateHasChanged();
        }

        bool isLoading = false;

        async Task ShowLoading()
        {
            isLoading = true;

            await Task.Yield();

            isLoading = false;
        }
    }
}