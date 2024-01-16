using WeatherForecastByLocation.Server.Dtos;

namespace WeatherForecastByLocation.Server.Models;

public class WeatherForecast(DateOnly Date, IEnumerable<Period> Periods)
{
    public DateOnly Date { get; } = Date;
    public IEnumerable<Period> Periods { get; } = Periods;
}
