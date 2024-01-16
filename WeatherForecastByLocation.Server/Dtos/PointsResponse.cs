namespace WeatherForecastByLocation.Server.Dtos;
public class PointsResponse
{
    public required PointsProperties Properties { get; init; }
    public string GetForecastUrl()
    {
        return Properties.Forecast;
    }

}

public class PointsProperties
{
    public required string Forecast { get; init; }
}
