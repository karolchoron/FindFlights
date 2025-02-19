using Microsoft.AspNetCore.Components;

public class HomeBase : ComponentBase
{
    [Inject] private NavigationManager Navigation { get; set; }

    protected void GoToFlightTracker()
    {
        Navigation.NavigateTo("/flight-tracker");
    }
}