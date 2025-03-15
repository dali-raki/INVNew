using INV.App.Suppliers;
using INV.Domain.Entities.Suppliers;
using Microsoft.AspNetCore.Components;

namespace INVUIs.Suppliers;

public partial class SupplierDeleteConformation
{
    [Parameter] public SupplierInfo supplierInfo { get; set; }
    [Inject] public ISupplierService SupplierService { get; set; }
    public bool display = false;
    private Supplier supplier;

    public void show()
    {
        display = true;
        StateHasChanged();
    }

    public void hide()
    {
        display = false;
        StateHasChanged();
    }

    private async Task submit(SupplierInfo supplierInfo)
    {
        supplier = new Supplier
        {
            Id = supplierInfo.ID,
        };
        await SupplierService.RemoveSupplierById(supplier.Id);
        //await OnConfirm.InvokeAsync(null);
        hide();
    }
}