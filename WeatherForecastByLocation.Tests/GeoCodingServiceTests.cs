using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using System.Web.Http;
using WeatherForecastByLocation.Server.Services.GeoCoding;

namespace WeatherForecastByLocation.Tests;

[TestClass]
public class GeoCodingServiceTests
{
    [TestMethod]
    public async Task ValidAddress_ShouldReturnLatAndLong()
    {
        var httpClient = new HttpClient();
        var sut = new GeoCodingService(httpClient);
        var result = await sut.GetLatAndLongByAddress("10000 San Leon Dr, Dickinson, TX 77539, Estados Unidos");
        Assert.IsNotNull(result);
        var (latitude, longitude) = result;
        Assert.IsNotNull(latitude);
        Assert.IsNotNull(longitude);
    }
    [TestMethod]
    public async Task InvalidAddress_ShouldReturnNotFound()
    {
        var httpClient = new HttpClient();
        var sut = new GeoCodingService(httpClient);
        try
        {
            await sut.GetLatAndLongByAddress("Invalid Address");
            Assert.Fail();
        }
        catch (HttpResponseException exception)
        {
            Assert.AreEqual(exception.Response.StatusCode, HttpStatusCode.NotFound);
            Assert.AreEqual(await exception.Response.Content.ReadAsStringAsync(), "Postal Address Not Found. Please enter another address.");

        }
    }
    [TestMethod]
    public async Task NullAddress_ShouldReturnBadRequest()
    {
        try
        {
            var httpClient = new HttpClient();
            var sut = new GeoCodingService(httpClient);
            await sut.GetLatAndLongByAddress("");
            Assert.Fail();
        }
        catch (HttpResponseException exception)
        {
            Assert.AreEqual(await exception.Response.Content.ReadAsStringAsync(), "Postal Address can't be empty. Please enter a valid address.");
            Assert.AreEqual(exception.Response.StatusCode, HttpStatusCode.BadRequest);
        }
    }
    [TestMethod]
    public async Task IfApiReturnsNull_ShouldThrowNotFound()
    {
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
        var sut = new GeoCodingService(httpClient);
        try
        {
            await sut.GetLatAndLongByAddress("Mock Address");
            Assert.Fail();
        }
        catch (HttpResponseException exception)
        {
            Assert.AreEqual(await exception.Response.Content.ReadAsStringAsync(), "Postal Address Not Found. Please enter another address.");
            Assert.AreEqual(exception.Response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}