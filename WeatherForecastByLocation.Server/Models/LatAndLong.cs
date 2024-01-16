namespace WeatherForecastByLocation.Server.Models;

public class LatAndLong(decimal Latitude, decimal Longitude)
{
    public decimal Latitude { get; } = Latitude;
    public decimal Longitude { get; } = Longitude;

    public void Deconstruct(out decimal latitude, out decimal longitude)
    {
        latitude = Latitude; longitude = Longitude;
    } 
}
