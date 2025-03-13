using INV.App.Suppliers;
using INV.Domain.Entities.Products;
using INV.Domain.Entities.Purchases;
using INV.Domain.Entities.Suppliers;
using INV.Implementation.Service.Purchses;
using Microsoft.AspNetCore.Components;

namespace INVUIs.Suppliers
{
    public partial class SupplierList
    {
        [Inject] private ISupplierService supplierService { get; set; }
        [Parameter] public List<SupplierInfo> Suppliers { get; set; } = new();
        private SupplierDeleteConformation supplierDeleteConformation;
        public List<SupplierInfo> supplierFilter { get; set; } = new();

        public SupplierInfo supplierDelete;
        private SupplierDeleteConformation deleteConfirmation;
        private string _searchName = "";
        [Inject] private NavigationManager navigationManager { get; set; }

        private string selectedLink(Guid id) => $"suppliers/{id}";

        private void NavigateToSupplierDetails(Guid supplierId)
        {
            navigationManager.NavigateTo($"/suppliers/{supplierId}");
        }

        protected override void OnParametersSet()
        {
            supplierFilter = Suppliers;
        }

        private SupplierForm supplierForm;

        private void showSupplierForm()
        {
            if (supplierForm != null)
            {
                supplierForm.ShowModal();
            }
        }

        private string searchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                getByName();
            }
        }

        private void getByName()
        {
            if (string.IsNullOrWhiteSpace(searchName))
            {
                supplierFilter = Suppliers.ToList();
                return;
            }

            string searchLower = searchName.Trim().ToLower();

            supplierFilter = Suppliers
                .Where(s => s.Name.ToLower().Contains(searchLower) || s.Email.ToLower().Contains(searchLower))
                .OrderBy(s => s.Name)
                .ToList();

            StateHasChanged();
        }

        public void NavigatePage()
        {
            navigationManager.NavigateTo("Supplier");
        }

        public void ShowModal()
        {
            StateHasChanged();
        }

        private async Task DeleteSupplier(SupplierInfo Suppliers)
        {
            supplierDeleteConformation.supplierInfo = Suppliers;
            supplierDeleteConformation.show();
            StateHasChanged();
        }
    }
}