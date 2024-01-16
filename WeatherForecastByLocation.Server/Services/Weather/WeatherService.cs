using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.Weather;

public class WeatherService(IForecastUrlService forecastUrlService, IForecastGridService forecastGridService) : IWeatherService
{
    private readonly IForecastGridService forecastGridService = forecastGridService;
    private readonly IForecastUrlService forecastUrlService = forecastUrlService;

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(LatAndLong latAndLong)
    {
        var url = await forecastUrlService.GetForecastUrl(latAndLong);
        return await forecastGridService.GetWeatherForecast(url);
    }
}
