using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

public class NavigationMenuBase : LayoutComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; }

    protected async Task HideMenu()
    {
        await JSRuntime.InvokeVoidAsync("hideNavbar");
    }
}