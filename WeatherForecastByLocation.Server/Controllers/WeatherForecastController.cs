using Microsoft.AspNetCore.Mvc;
using WeatherForecastByLocation.Server.Services.GeoCoding;
using WeatherForecastByLocation.Server.Services.Weather;

namespace WeatherForecastByLocation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IGeoCodingService geoCodingService, IWeatherService weatherService) : ControllerBase
    {
        
        private readonly IGeoCodingService geoCodingService = geoCodingService;
        private readonly IWeatherService weatherService = weatherService;

        [HttpGet("{postalAddress}")]
        public async Task<IActionResult> Get([FromRoute] string postalAddress = "12020 Pepperidge Dr, Austin, TX 78739, EUA")
        {
            var latAndLong = await geoCodingService.GetLatAndLongByAddress(postalAddress);
            var result = await weatherService.GetWeatherForecast(latAndLong);
            return Ok(result);
        }
    }
}
