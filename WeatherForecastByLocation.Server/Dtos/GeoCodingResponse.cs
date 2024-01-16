using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Dtos;

public class GeoCodingResponse
{
    public required GeoCodingResult Result { get; init; }
    public LatAndLong? GetLatAndLong()
    {
        var matches = Result.AddressMatches;
        if (matches.Length > 0)
        {
            var (x, y) = matches[0].Coordinates;
            return new LatAndLong(Latitude: y, Longitude: x);
        }
        return null;
    }
}

public class GeoCodingResult
{
    public required AddressMatch[] AddressMatches { get; init; }
}

public class AddressMatch
{
    public required Coordinates Coordinates { get; init; }

}
public class Coordinates
{
    public decimal X { get; init; }
    public decimal Y { get; init; }
    public void Deconstruct(out decimal x, out decimal y)
    {
        x = X;
        y = Y;
    }
}