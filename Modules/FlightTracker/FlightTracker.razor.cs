using FindFlights.Modules.FlightTracker;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class FlightTrackerBase : ComponentBase
{
    [Inject] private HttpClient Http { get; set; }

    protected string? FlightNumber { get; set; }
    protected FlightInfo? FlightData { get; set; }
    protected string? ErrorMessage { get; set; }

    protected async Task SearchFlight()
    {
        if (!string.IsNullOrEmpty(FlightNumber))
        {
            // URL do zapytania API
            
            // TODO - API keys -> secrets
            var url = $"https://api.aviationstack.com/v1/flights?access_key=XXX_PUT_HERE_YOUR_API_KEY_XXX&flight_iata={FlightNumber}";

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
                    ErrorMessage = ""; // czyszczenie error message z poprzednich zapytań
                }
                else
                {
                    ErrorMessage = "Nie znaleziono lotu o podanym numerze.";
                    return;
                }
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("403"))
            {
                ErrorMessage = "Brak dostępu do tej funkcji w wybranym planie subskrypcyjnym. Poinformuj administratora strony.";
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                ErrorMessage = "Zasób nie został znaleziony. Sprawdź numer lotu.";
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("500"))
            {
                ErrorMessage = "Wystąpił błąd wewnętrzny serwera. Spróbuj ponownie później.";
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("401"))
            {
                ErrorMessage = "Nieprawidłowy klucz API. Sprawdź swój klucz dostępu.";
            }
            // Glowne bledy:
            catch (HttpRequestException ex) when (ex.Message.Contains("429"))
            {
                ErrorMessage = "Przekroczono limit zapytań do API. Skontaktuj się z administratorem lub spróbuj ponownie później.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Wystąpił nieoczekiwany błąd: {ex.Message}";
            }
        }
    }

    // Zmiana formatu daty, bez zmiany strefy czasowej
    protected string FormatDate(string scheduledTime)
    {
        if (DateTimeOffset.TryParse(scheduledTime, out DateTimeOffset parsedDate))
        {
            return parsedDate.ToString("dd MMMM yyyy, HH:mm");
        }
        return "Brak informacji";
    }
}