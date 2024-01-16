using System.Net;
using System.Web.Http;
using WeatherForecastByLocation.Server.Dtos;
using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.Weather;

public class ForecastService(HttpClient httpClient) : IForecastGridService, IForecastUrlService
{
    protected static readonly HttpResponseException notFoundException = new(new HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound, Content = new StringContent("Forecast Not Found. Please enter another address.") });
    protected readonly HttpClient httpClient = SetupClient(httpClient);
    protected const string baseUrl = "https://api.weather.gov";
 
    private static HttpClient SetupClient(HttpClient client)
    {
        client.DefaultRequestHeaders.Add("User-Agent", "MyForecastApp/v1.0 (marcelo; mpbguima@hotmail.com)");
        return client;
    }

    public async Task<string> GetForecastUrl(LatAndLong latAndLong)
    {
        var (latitude, longitude) = latAndLong;
        var response = await httpClient.GetAsync($"{baseUrl}/points/{latitude},{longitude}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PointsResponse>();
        return result?.GetForecastUrl() ?? throw notFoundException;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast(string url)
    {
        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<ForecastResponse>();
        response.EnsureSuccessStatusCode();
        return result?.MapToWeatherForecasts() ?? throw notFoundException;
    }
}
