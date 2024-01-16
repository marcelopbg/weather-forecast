using Moq.Protected;
using Moq;
using System.Net;
using WeatherForecastByLocation.Server.Models;
using WeatherForecastByLocation.Server.Services.GeoCoding;
using WeatherForecastByLocation.Server.Services.Weather;
using System.Web.Http;
using System;

namespace WeatherForecastByLocation.Tests;

[TestClass]
public class WeatherServiceTests
{
    [TestMethod]
    public async Task LatAndLongWithinUS_ShouldReturnForecast()
    {
        var httpClient = new HttpClient();
        var service = new ForecastService(httpClient);
        WeatherService sut = new(service, service);
        var input = new LatAndLong(40, -100);
        var result = await sut.GetWeatherForecast(input);
        Assert.AreEqual(result.Count(), 7);
    }
    [TestMethod]
    public async Task LatAndLongOutOfBounds_ShouldThrow()
    {
        var httpClient = new HttpClient();
        var service = new ForecastService(httpClient);
        WeatherService sut = new(service, service);
        var input = new LatAndLong(-27.02563913056792M, -48.6664357564631M);
        try
        {
            await sut.GetWeatherForecast(input);
            Assert.Fail();
        }
        catch (HttpRequestException e)
        {
            Assert.AreEqual(e.StatusCode, HttpStatusCode.NotFound);
        }
    }

    [TestMethod]
    public async Task IfUrlReturnsAndForecastGridFails_ShouldThrow()
    {
        Mock<IForecastUrlService> forecastUrlServiceMock = new(MockBehavior.Strict);
        forecastUrlServiceMock
            .Setup(oo => oo.GetForecastUrl(It.IsAny<LatAndLong>()))
            .ReturnsAsync("http://localhost:11111");

        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent("null")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new WeatherService(forecastUrlServiceMock.Object, new ForecastService(httpClient));
        try
        {
            await sut.GetWeatherForecast(new LatAndLong(40, -100));
            Assert.Fail();
        }
        catch (HttpResponseException e)
        {
            Assert.AreEqual(e.Response.StatusCode, HttpStatusCode.NotFound);
            Assert.AreEqual(await e.Response.Content.ReadAsStringAsync(), "Forecast Not Found. Please enter another address.");

        }
    }
}
