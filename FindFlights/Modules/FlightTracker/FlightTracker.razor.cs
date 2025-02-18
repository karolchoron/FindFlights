using FindFlights.Modules.FlightTracker;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FindFlights.Modules.FlightTracker.Models;
using static System.Net.WebRequestMethods;

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
            var url = $"http://localhost:5240/api/flighttracker/flightinfo?flightNumber={FlightNumber}"; // URL do API backendu

            try
            {
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
                }
            }

            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                {
                    ErrorMessage = "Błąd autoryzacji: Nieprawidłowy klucz API. Skontakuj się z administratorem.";
                }
                else if (ex.Message.Contains("403"))
                {
                    ErrorMessage = "Dostęp zabroniony: Funkcja nie jest dostępna na obecnym planie subskrypcji. Skontakuj się z administratorem.";
                }
                else if (ex.Message.Contains("404"))
                {
                    ErrorMessage = "Nie znaleziono żądanej funkcji API. Skontakuj się z administratorem.";
                }
                else if (ex.Message.Contains("429"))
                {
                    ErrorMessage = "Przekroczono limit zapytań. Spróbuj ponownie później lub skontakuj się z administratorem.";
                }
                else if (ex.Message.Contains("500"))
                {
                    ErrorMessage = "Wewnętrzny błąd serwera. Spróbuj ponownie później.";
                }
                else
                {
                    ErrorMessage = $"Wystąpił błąd: {ex.Message}";
                }
            }
        }
    }

    // Zmiana formatu daty - nie zależnie od stref czasowych
    protected string FormatDate(string scheduledTime)
    {
        if (DateTimeOffset.TryParse(scheduledTime, out DateTimeOffset parsedDate))
        {
            return parsedDate.ToString("dd MMMM yyyy, HH:mm");
        }
        return "Brak informacji";
    }
}