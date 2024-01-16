using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.Weather;

public interface IForecastUrlService
{
    public Task<string> GetForecastUrl(LatAndLong latAndLong);

}
