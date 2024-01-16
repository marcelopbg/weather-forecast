using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Dtos;
public class ForecastResponse
{
    public required ForecastProperties Properties { get; init; }
    public IEnumerable<WeatherForecast> MapToWeatherForecasts()
    {
        return Properties.Periods.GroupBy(p => DateOnly.FromDateTime(p.StartTime)).Select(g => new WeatherForecast(Date: g.Key, g.ToList()));
    }
    public class ForecastProperties
    {
        public required Period[] Periods { get; init; }
    }
}

public class Period
{
    public required string Name { get; init; }
    public required int Temperature { get; init; }
    public required string TemperatureUnit { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required string Icon { get; init; }
    public required string ShortForecast { get; init; }
    public string? LongForecast { get; init; }
    public required bool IsDayTime { get; init; }
    public required string WindSpeed { get; init; }
    public required string WindDirection { get; init; }
    public required Precipitation ProbabilityOfPrecipitation { get; init; }
    public required Humidity RelativeHumidity { get; init; }
}
public class Precipitation
{
    public byte? Value { get; init; }
}
public class Humidity
{
    public byte? Value { get; init; }
}

