using FindFlights.Modules.FlightTracker;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class FlightTrackerBase : ComponentBase
{
    [Inject] private HttpClient Http { get; set; }

    protected string FlightNumber { get; set; }
    protected FlightInfo FlightData { get; set; }

    protected async Task SearchFlight()
    {
        if (!string.IsNullOrEmpty(FlightNumber))
        {
            // TODO - API ACCESS KEY
            // TODO - obsluga bledu - przekroczony limit zapytan API

            var url = $"https://api.aviationstack.com/v1/flights?access_key=XXXPUT_HERE_API_ACCES_KEYXXX&flight_iata={FlightNumber}";

            var response = await Http.GetFromJsonAsync<FlightApiResponse>(url);
            Console.WriteLine($"response: {response}");

            if (response?.Data?.Count > 0)
            {
                FlightData = response.Data[0];

                Console.WriteLine($"Flight Status: {FlightData.flight_status}");
            }
            // ELSE - todo - obsluga bledow - jesli brak odp to ze np zly numer lotu
            // TODO 2 - jesli error - zrob try catch bo inna struktura - to wtedy wyswietl informacje "nieoczekiwany blad - sprobuj ponownie pozniej" albo "przekrocozno liczbe zapytan - sproboj ponownie ponziej"
        }
    }

    // change datatime format, without changing the time zones
    protected string FormatDate(string scheduledTime)
    {
        if (DateTimeOffset.TryParse(scheduledTime, out DateTimeOffset parsedDate))
        {
            return parsedDate.ToString("dd MMMM yyyy, HH:mm");
        }
        return "Brak informacji";
    }
}