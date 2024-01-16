using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.Weather;

public interface IForecastGridService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecast(string url);
}
