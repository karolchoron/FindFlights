using Microsoft.AspNetCore.Components;

public class SearchBase : ComponentBase
{
    protected string DepartureAirport { get; set; }
    protected string ArrivalAirport { get; set; }
    protected DateTime? DepartureDate { get; set; }
    protected DateTime? ReturnDate { get; set; }
    protected bool ShowMessage { get; set; } = false;

    protected void ShowComingSoonMessage()
    {
        ShowMessage = true;
    }
}