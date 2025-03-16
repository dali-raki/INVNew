using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace INVUIs.Shared;

public class MyAlert(IJSRuntime jsRuntime)
{
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