using FindFlights.Modules.FlightTracker;

public class FlightInfo
{
    public string FlightStatus { get; set; }
    public Airline Airline { get; set; }
    public Airport Departure { get; set; }
    public Airport Arrival { get; set; }
}
