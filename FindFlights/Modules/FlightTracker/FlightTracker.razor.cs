using FindFlights.Modules.FlightTracker;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FindFlights.Modules.FlightTracker.Models;

public class FlightTrackerBase : ComponentBase
{
    [Inject] private HttpClient Http { get; set; }

    protected string? FlightNumber { get; set; }
    protected FlightData? FlightData { get; set; }
    protected string? ErrorMessage { get; set; }

    protected async Task SearchFlight()
    {
        if (!string.IsNullOrEmpty(FlightNumber))
        {
            // URL do API backendu
            var url = $"http://localhost:5240/api/flighttracker/flightinfo?flightNumber={FlightNumber}"; // HTTP
 
            try
            {
                // Wysłanie zapytania do lokalnego API
                var response = await Http.GetFromJsonAsync<FlightApiResponse>(url);

                if (response == null)
                {
                    ErrorMessage = "Nieoczekiwany błąd - spróbuj ponownie później.";
                    return;
                }

                if (response?.data?.Count > 0)
                {
                    FlightData = response.data[0];
                    ErrorMessage = ""; // czyszczenie komunikatu o błędzie
                }
                else
                {
                    ErrorMessage = "Nie znaleziono lotu o podanym numerze.";
                    return;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Wystąpił błąd: {ex.Message}";
            }
        }
    }

    // Zmiana formatu daty
    protected string FormatDate(string scheduledTime)
    {
        if (DateTimeOffset.TryParse(scheduledTime, out DateTimeOffset parsedDate))
        {
            return parsedDate.ToString("dd MMMM yyyy, HH:mm");
        }
        return "Brak informacji";
    }
}
