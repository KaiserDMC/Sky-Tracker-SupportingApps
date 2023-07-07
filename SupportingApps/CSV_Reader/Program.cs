using CsvHelper;
using System.Globalization;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("F:\\SoftUni\\09. ASP .NET Advanced\\03. Project Data\\20210723_flights.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var flights = csv.GetRecords<Flight>().ToList();
                int minimumFlightsPerDay = 3;

                // Filter flights that have the same aircraft_id and registration and flew at least twice
                var filteredFlights = flights
                    .Where(f => !f.Equipment.Contains("C1") && (f.ScheduledDeparture != f.RealArrival) && (f.ScheduledArrival == f.RealArrival)
                                && !string.IsNullOrEmpty(f.Registration) && !string.IsNullOrEmpty(f.Equipment))
                    .GroupBy(f => new { f.AircraftId, f.Registration })
                    .Where(g => g.Count() >= minimumFlightsPerDay)
                    .SelectMany(g => g);

                // Save the filtered flights to the database
                //foreach (var flight in filteredFlights)
                //{
                //    Console.WriteLine(flight.ToString());
                //}

                //Console.WriteLine($"Flight Count: {filteredFlights.Count()}");
                //Console.WriteLine($"Planes Count: {filteredFlights.Count() / minimumFlightsPerDay}");

                var aircraft = filteredFlights
                .Select(f => new AircraftSeedDto
                {
                    AircraftId = f.AircraftId,
                    Registration = f.Registration,
                    Equipment = f.Equipment
                })
                .Distinct()
                .ToHashSet();

                Console.WriteLine(string.Join(Environment.NewLine, aircraft.ToString()));
            }
        }
    }
}