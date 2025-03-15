using INV.App.Products;
using Microsoft.AspNetCore.Components;

namespace INV.Web.Components.Pages.Products;

public partial class ProductDetailPage
{
    [Parameter] public Guid ProductId { get; set; }
    private ProductInfo? product;

    protected override async Task OnInitializedAsync()
    {
        var result = await ProductService.GetProductById(ProductId);
        if (result.IsSuccess)
        {
            product = result.Value;
        }
        StateHasChanged();
    }
}