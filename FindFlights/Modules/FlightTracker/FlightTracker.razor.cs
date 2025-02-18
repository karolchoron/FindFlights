using FindFlights.Modules.FlightTracker;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
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
            // URL do lokalnego API
            var url = $"http://localhost:5240/api/flighttracker/flightinfo?flightNumber={FlightNumber}"; // HTTP

            Console.WriteLine($"!!! KLIENT url klient: {url}");
            Console.WriteLine($"!!! KLIENT  FlightNumber klient: {FlightNumber}");
            
            try
            {
                // Wysłanie zapytania do lokalnego API
                var response = await Http.GetFromJsonAsync<FlightApiResponse>(url);

                Console.WriteLine($"!!! KLIENT  response: {response}");



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


    //// Klasa pomocnicza do odbierania odpowiedzi API
    //public class DoubleResponse
    //{
    //    public int original { get; set; }
    //    public int doubled { get; set; }
    //}


    //public int InputNumber { get; set; }
    //public int? DoubledResult { get; set; }

    //protected async Task DoubleNumber()
    //{


    //    // HTTP
    //    var url = $"http://localhost:5240/api/flighttracker/double?number={InputNumber}";
    //    Console.WriteLine($"!!! KLIENT Wysyłanie do API: {url}");

  
    //    try
    //    {
    //        Console.WriteLine("!!! KLIENT Sending request to API");
    //        var response = await Http.GetFromJsonAsync<DoubleResponse>(url);
    //        Console.WriteLine("!!! KLIENT Received response from API");
    //        //var response = await Http.GetFromJsonAsync<DoubleResponse>(url);


    //        Console.WriteLine($"!!! KLIENTresponse: {response}");
            

    //        if (response != null)
    //        {
    //            DoubledResult = response.doubled;
    //            Console.WriteLine($"!!! KLIENT Odpowiedź z API: {DoubledResult}");
    //        }
    //        else
    //        {
    //            Console.WriteLine("!!! KLIENT Błąd: Pusta odpowiedź");
    //        }
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"!!! KLIENT Błąd: {ex.Message}");
    //    }
    //}




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
