using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.Weather;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecast(LatAndLong latAndLong);
}
