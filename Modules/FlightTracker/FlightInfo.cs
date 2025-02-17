﻿using FindFlights.Modules.FlightTracker;

public class FlightInfo
{
    public string flight_status { get; set; } // wymagana inna notacja niż appercase, aby prawidłowo zmapować odpowiedzi z API JSON na zmienne
    public Airline airline { get; set; }
    public Airport departure { get; set; }
    public Airport arrival { get; set; }
}
