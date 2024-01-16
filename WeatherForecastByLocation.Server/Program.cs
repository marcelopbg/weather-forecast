using WeatherForecastByLocation.Server;
using WeatherForecastByLocation.Server.Services.GeoCoding;
using WeatherForecastByLocation.Server.Services.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IGeoCodingService, GeoCodingService>();
builder.Services.AddHttpClient<IForecastUrlService, ForecastService>();
builder.Services.AddHttpClient<IForecastGridService, ForecastService>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
