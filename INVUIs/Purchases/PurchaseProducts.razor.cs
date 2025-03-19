using System.Runtime.CompilerServices;
using INV.App.Products;
using INV.App.Purchases;
using INV.Domain.Entities.Purchases;
using INV.Implementation.Service.Products;
using INVUIs.Components.Status;
using INVUIs.Products;
using INVUIs.Products.ProductsModel;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace INVUIs.Purchases;

public partial class PurchaseProducts : ComponentBase
{
    [CascadingParameter] public List<ProductModel> products { set; get; } = new();
    [Parameter] public EventCallback<List<ProductModel>> OnProductAddProduct { get; set; }
    [Parameter] public PurchaseOrder PurchaseInfo { get; set; }
    [Parameter] public EventCallback<ProductModel> OnEditProduct { get; set; }
    [Parameter] public List<ProductModel> ProductList { get; set; }
    [Inject] private IPurchaseOrderService purchaseOrderService { get; set; }

    private List<int> TVAOptions = new() { 9, 19 };
    private List<string> UnitMesures = new() { "U", "KG", "M", "L" };
    public RadzenDataGrid<ProductModel> grid;
    private ProductEditForm productEditForm;
    private ProductForm productForm = new();
    public ProductSelector productSelector = new();
    private ProductModel? selectedProductModel = null;
    public ProductModel productModel { get; set; }

    private bool showEditPopup = false;
    private bool showPopup = false;
    private bool Display = false;

    private bool StatusButton(PurchaseOrder purchaseOrder)
    {
        if (purchaseOrder == null || purchaseOrder.Status == PurchaseStatus.Editing)
        {
            return true;
        }
        return false;
    }

    protected override void OnParametersSet()
    {
        if (ProductList != null)
        {
            products = new List<ProductModel>(ProductList);
        }
    }

    private async Task DeleteProduct(ProductModel product)
    {
        products.Remove(product);
        for (var i = 0; i < products.Count; i++) products[i].Number = i + 1;

        OnProductAddProduct.InvokeAsync(products);
        await purchaseOrderService.RemovePurchaseProduct(product.ID, product.IDPurchaseOrder);
    }

    private void Clear() => productModel = new ProductModel();

    private void closePopup()
    {
        showPopup = false;
        StateHasChanged();
        Clear();
    }

    private async Task AddProductToGrid(ProductModel product)
    {
        // product.Number = products.Count + 1;
        product.TotalPrice = product.Quantity * product.UnitPrice;
        if (PurchaseInfo is not null)
        {
            var purchaseProduct = new PurchaseProduct
            {
                ProductId = product.ID,
                PurchaseOrderId = PurchaseInfo.Id,
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice,
            };
            await purchaseOrderService.CreateProductPurchase(purchaseProduct);
        }
        else
        {
            var purchaseProduct = new PurchaseProduct
            {
                ProductId = product.ID,
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice,
            };
        }

        products.Add(product);
        StateHasChanged();
    }

    private void CloseEditPopup()
    {
        showEditPopup = false;
        StateHasChanged();
    }

    private async Task EditProduct(ProductModel product)
    {
        selectedProductModel = new ProductModel
        {
            ID = product.ID,
            IDPurchaseOrder = product.IDPurchaseOrder,
            Designation = product.Designation,
            UnitMeasure = product.UnitMeasure,
            Quantity = product.Quantity,
            UnitPrice = product.UnitPrice,
            TVA = product.TVA
        };
        productEditForm.show();
        StateHasChanged();
    }

    private async Task SaveEditedProduct()
    {
        if (selectedProductModel != null)
        {
            var product = products.FirstOrDefault(p => p.ID == selectedProductModel.ID);
            if (product != null)
            {
                product.Quantity = selectedProductModel.Quantity;
                product.UnitPrice = selectedProductModel.UnitPrice;
                product.TotalPrice = product.Quantity * product.UnitPrice;
            }
            showEditPopup = false;
            var purchaseProduct = new PurchaseProduct
            {
                ProductId = product.ID,
                PurchaseOrderId = product.IDPurchaseOrder,
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice,
            };
            await purchaseOrderService.UpdatePurchaseProduct(purchaseProduct);
            StateHasChanged();
        }
    }

    public async void loadtab()
    {
        await grid.Reload();
    }

    private decimal getTHT()
    {
        return products.Sum(p => p.UnitPrice * p.Quantity);
    }

    private decimal getTVA()
    {
        return products.Sum(p => p.UnitPrice * p.Quantity * p.TVA) / 100;
    }

    private decimal getTTC()
    {
        return getTHT() + getTVA();
    }
}