using Microsoft.JSInterop;

namespace INVUIs.Shared;

public class MyAlert
{
    private readonly IJSRuntime jsRuntime;

    public MyAlert(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }
    public async Task ShowErrorAlert(string title, string message)
    {
        await jsRuntime.InvokeVoidAsync("Swal.fire", new
        {
            title,
            html = message,
            icon = "error",
            confirmButtonText = "OK"
        });
    }
}