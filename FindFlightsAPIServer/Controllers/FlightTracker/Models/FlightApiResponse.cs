using FindFlightsAPI.Controllers;

namespace FindFlightsAPIServer.Controllers.FlightTracker.Models
{
    public class FlightApiResponse
    {
        public List<FlightData> data { get; set; }
    }
}