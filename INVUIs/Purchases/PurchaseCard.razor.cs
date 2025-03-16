using INV.App.Purchases;
using INV.Domain.Entities.Purchases;
using Microsoft.AspNetCore.Components;

namespace INVUIs.Purchases;

public partial class PurchaseCard
{
    [Parameter] public PurchaseOrder purchaseInfo { set; get; }
    [Inject] public IPurchaseOrderService purchaseOrderService { get; set; }
    private bool displayVisa = false;
    private bool displayReject = false;

    public void visa(PurchaseOrder purchaseInfo)
    {
        purchaseInfo.Status = PurchaseStatus.Visi;
        purchaseOrderService.UpdatePurchaseOrder(purchaseInfo);
    }

    public void reject(PurchaseOrder purchaseInfo)
    {
        purchaseInfo.Status = PurchaseStatus.Reject;
        purchaseOrderService.UpdatePurchaseOrder(purchaseInfo);
    }

    public void ShowVisa()
    {
        displayVisa = true;
        StateHasChanged();
    }

    public void HideVisa()
    {
        purchaseInfo.VisaDate = null;
        purchaseInfo.VisaNumber = null;
        displayVisa = false;
        StateHasChanged();
    }

    public void Showreject()
    {
        displayReject = true;
        StateHasChanged();
    }

    public void HideReject()
    {
        purchaseInfo.VisaDate = null;
        purchaseInfo.VisaNumber = null;
        purchaseInfo.Observation = null;
        displayReject = false;
        StateHasChanged();
    }
}