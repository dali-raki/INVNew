using INV.App.Products;
using INV.Domain.Entities.Products;
using Microsoft.AspNetCore.Components;

namespace INV.Web.Components.Pages.Products;

public partial class ProductsListPage
{
    [Inject] public IProductService productService { get; set; }
    private List<Product> products;

    protected override async Task OnInitializedAsync()
    {
        var result = await productService.GetProducts();
        if (result.IsSuccess)
        {
            products = result.Value;
        }

        StateHasChanged();
    }
}