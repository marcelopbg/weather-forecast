using System.Net;
using System.Web.Http;
using WeatherForecastByLocation.Server.Dtos;
using WeatherForecastByLocation.Server.Models;

namespace WeatherForecastByLocation.Server.Services.GeoCoding;

public class GeoCodingService(HttpClient httpClient) : IGeoCodingService
{
    private readonly HttpClient httpClient = httpClient;

    private static readonly string geoCodingBaseAddress = "https://geocoding.geo.census.gov/geocoder/locations/onelineaddress?benchmark=4&format=json";
    public async Task<LatAndLong> GetLatAndLongByAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, Content = new StringContent("Postal Address can't be empty. Please enter a valid address.") });
        var url = $"{geoCodingBaseAddress}&address={address}";
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<GeoCodingResponse>();
        var latAndLong = result?.GetLatAndLong();
        return latAndLong ?? throw new HttpResponseException(new HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound, Content = new StringContent("Postal Address Not Found. Please enter another address.") });
    }
}
