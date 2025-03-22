using INV.App.Products;
using INV.Domain.Entities.Products;
using INV.Domain.Shared;
using INV.Shared;
using INVUIs.Products.ProductsModel;
using INVUIs.Purchases.PurchaseModels;
using INVUIs.Shared.Models;
using INVUIs.WareHouses.Models;
using Microsoft.AspNetCore.Components;

namespace INVUIs.Products;

public partial class ProductForm : ComponentBase
{
    [Parameter] public EventCallback<ProductInfo> OnProductCreated { get; set; }
    [Parameter] public ProductDetail productEdit { get; set; }

    [Parameter] public RenderFragment Pills { get; set; }
    [Inject] private IProductService productService { set; get; }
    [Inject] private NavigationManager navigationManager { set; get; }

    public ProductForm productForm;

    public FormState formState;
    public ProductModel productModel = new ProductModel();

    private Result result;
    private string message = string.Empty;
    private List<int> TVAOptions = new() { 9, 19 };

    private List<WareHouseModel> WareHouse = new()
{
    new WareHouseModel { Id = Guid.NewGuid(), WareHouseName = "Chetma" },
    new WareHouseModel { Id = Guid.NewGuid(), WareHouseName = "Biskra" }
};

    private List<string> UnitMesures = new() { "U", "KG", "M", "L" };
    private bool visibility = false;
    private string succesMessage => formState == FormState.Create ? "Product created" : "Product updated";

    public async Task SubmitProduct()
    {
        var product = new ProductInfo
        {
            Id = productModel.ID,
            UnitMeasure = productModel.UnitMeasure,
            Designation = productModel.Designation,
            TVA = productModel.TVA,
            DefaultWareHouseId = productModel.WareHouseId
        };
        if (formState == FormState.Create)
        {
            var productadd = new Product
            {
                Id = productModel.ID,
                UnitMeasure = productModel.UnitMeasure,
                Designation = productModel.Designation,
                TVA = productModel.TVA,
                DefaultWareHouseId = productModel.WareHouseId
            };
            result = await productService.CreateProduct(productadd);
        }
        else
        {
            var productupdate = new Product
            {
                Id = productModel.ID,
                UnitMeasure = productModel.UnitMeasure,
                Designation = productModel.Designation,
                TVA = productModel.TVA,
                DefaultWareHouseId = productModel.WareHouseId
            };

            result = await productService.SetProducts(productupdate);
        }

        if (result.IsSuccess)
        {
            await OnProductCreated.InvokeAsync(product);
            Hide();
        }
        else message = result.Error.Description;
    }

    private void clearForm()
    {
        productModel = new();
        message = string.Empty;
    }

    public void ShowModal()
    {
        visibility = true;
        StateHasChanged();
    }

    public void Hide()
    {
        clearForm();
        visibility = false;
        StateHasChanged();
    }
}