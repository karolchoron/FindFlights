using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public class NavigationMenuBase : LayoutComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; }

    protected async Task HideMenu()
    {
        await JSRuntime.InvokeVoidAsync("hideNavbar");
    }
}