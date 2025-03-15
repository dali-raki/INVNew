using INV.App.Suppliers;
using Microsoft.AspNetCore.Components;

namespace INV.Web.Components.Pages.Suppliers
{
    public partial class SupplierListPage : ComponentBase
    {
        [Inject] private ISupplierService supplierService { get; set; }

        private List<SupplierInfo> suppliers;

        private string texte = DateTime.Now.ToString("dd/MM/yyyy HH:ss");

        protected override async Task OnInitializedAsync()
        {
            var result = await supplierService.GetAllSupplier();
            if (result.IsSuccess)
            {
                suppliers = result.Value;
            }
        }
    }
}