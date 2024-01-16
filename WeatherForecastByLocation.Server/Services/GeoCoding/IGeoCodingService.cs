using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.GeoCoding;

public interface IGeoCodingService
{
    Task<LatAndLong> GetLatAndLongByAddress(string address);
}
