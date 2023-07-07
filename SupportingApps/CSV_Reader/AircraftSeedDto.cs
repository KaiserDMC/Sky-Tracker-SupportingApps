namespace ConsoleApp2;

public class AircraftSeedDto
{
    public string AircraftId { get; set; } = null!;

    public string? Registration { get; set; }

    public string? Equipment { get; set; }

    public override string ToString()
    {
        return $"{AircraftId} {Registration ?? "NaN"} {Equipment ?? "NaN"}";
    }
}