using FindFlights.Modules.FlightTracker;

public class FlightInfo
{
    public string flight_status { get; set; }
    public Airline airline { get; set; }
    public Airport departure { get; set; }
    public Airport arrival { get; set; }
}
