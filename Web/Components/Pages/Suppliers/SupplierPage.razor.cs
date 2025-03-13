using INV.App.Purchases;
using INV.App.Receipts;
using INV.App.Services;
using INV.App.Suppliers;
using INV.Domain.Entities.Receipts;
using INV.Web.Services.Suppliers;
using INVUIs.Suppliers;
using INVUIs.Suppliers.Models;
using Microsoft.AspNetCore.Components;

namespace INV.Web.Components.Pages.Suppliers
{
    public partial class SupplierPage
    {
        [Parameter] public Guid id { get; set; }
        private SupplierDetail Supplier { get; set; }
        private List<PurchaseOrderInfo> purchases;
        private List<ReceiptInfo> Receptions;

        public SupplierForm supplierForm = new SupplierForm();
        [Inject] public IAppSupplierService serviceSupplier { set; get; }
        [Inject] public IPurchaseOrderService purchaseOrderService { get; set; }
        [Inject] public IReceiptService receiptService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadSupplierData();
        }

        private async Task LoadSupplierData()
        {
            try
            {
                Supplier = await serviceSupplier.GetSupplierDetail(id);
                purchases = await purchaseOrderService.GetPurchaseOrdersByIdSupplier(id);
                Receptions = await receiptService.GetReceiptsBySupplierId(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void EditSupplier()
        {
            supplierForm.SupplierToEdit = new SupplierModel
            {
                ID = Supplier.Id,
                NameSupplier = Supplier.ManagerName,
                NameCompany = Supplier.CompanyName,
                Email = Supplier.Email,
                Address = Supplier.Address,
                Phone = Supplier.Phone,
                ART = Supplier.ART,
                NIF = Supplier.NIF,
                RC = Supplier.RC,
                NIS = Supplier.NIS,
                RIB = Supplier.RIB,
                BankAgency = Supplier.BankAgency
            };
            supplierForm.Update = true;
            supplierForm.ShowModal();
        }

        private async Task OnSupplierSaved(SupplierInfo updatedSupplier)
        {
            await LoadSupplierData();
        }
    }
}