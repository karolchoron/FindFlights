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
            // TODO - dodaj secrets keys json

            var url = $"https://api.aviationstack.com/v1/flights?access_key=_PUT_HERE_API_ACCES_KEY_&flight_iata={FlightNumber}";

            var response = await Http.GetFromJsonAsync<FlightApiResponse>(url);

            if (response?.Data?.Count > 0)
            {
                FlightData = response.Data[0];
            }
        }
    }
}
