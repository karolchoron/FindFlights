using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FindFlightsAPIServer.Controllers.FlightTracker.Models;
    
namespace FindFlightsAPI.Controllers
{
    [Route("api/flighttracker")]
    [ApiController]
    public class FlightTrackerController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public FlightTrackerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("flightinfo")]
        public async Task<IActionResult> GetFlightData(string flightNumber)
        {
            if (string.IsNullOrEmpty(flightNumber))
            {
                return BadRequest("Numer lotu nie mo¿e byæ pusty");
            }

            // TODO tymczasowo zmienne key api
            var url = $"http://api.aviationstack.com/v1/flights?access_key=XXX_PUT_HERE_YOU_API_KEY_XXX&flight_iata={flightNumber}";

            try
            {
                // Wywo³anie zewnêtrznego API - aviationstack
                var response = await _httpClient.GetStringAsync(url);

                // Jeœli odpowiedŸ jest pusta
                if (string.IsNullOrEmpty(response))
                {
                    return NotFound("Nie znaleziono danych o locie.");
                }

                return Ok(response); // Zwroc dane z API aviationstack
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"B³¹d podczas ³¹czenia siê z API: {ex.Message}");
            }
        }
    }
}