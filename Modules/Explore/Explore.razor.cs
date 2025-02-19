using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public partial class ExploreBase : ComponentBase
{
    [Inject] private IJSRuntime JSRuntime { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("initializeLeafletMap");
        }
    }
}