using CsvHelper.Configuration.Attributes;

namespace ConsoleApp2;

public class Flight
{
    [Name("flight_id")]
    public string FlightId { get; set; }

    [Name("aircraft_id")]
    public string AircraftId { get; set; }

    [Name("reg")]
    public string? Registration { get; set; }

    [Name("equip")]
    public string? Equipment { get; set; }

    [Name("callsign")]
    public string Callsign { get; set; }

    [Name("flight")]
    public string? FlightNumber { get; set; }

    [Name("schd_from")]
    public string ScheduledDeparture { get; set; }

    [Name("schd_to")]
    public string? ScheduledArrival { get; set; }

    [Name("real_to")]
    public string? RealArrival { get; set; }

    [Name("reserved")]
    public string? Reserved { get; set; }

    public override string ToString()
    {
        return $"{FlightId} {AircraftId} {Registration ?? "NaN"} {Equipment ?? "NaN"} " +
               $"{Callsign} {FlightNumber ?? "NaN"} " +
               $"{ScheduledDeparture} " +
               $"{ScheduledArrival} " +
               $"{RealArrival ?? "NaN"} " +
               $"{Reserved ?? "NaN"}";
    }
}