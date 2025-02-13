using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class SearchBase : ComponentBase
{
    protected string DepartureAirport { get; set; } = "";
    protected string ArrivalAirport { get; set; } = "";
    protected DateTime DepartureDate { get; set; } = DateTime.Today;
    protected DateTime? ReturnDate { get; set; }

    protected List<Flight> Flights { get; set; }

    protected async Task SearchFlights()
    {
        await Task.Delay(500); // Symulacja pobierania danych TODO

        Flights = new List<Flight>
        {
            new Flight { DepartureAirport = DepartureAirport, ArrivalAirport = ArrivalAirport, DepartureDate = DepartureDate, Price = 1200 },
            new Flight { DepartureAirport = DepartureAirport, ArrivalAirport = ArrivalAirport, DepartureDate = DepartureDate, Price = 1400 }
        };
    }

    protected class Flight
    {
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Price { get; set; }
    }
}
