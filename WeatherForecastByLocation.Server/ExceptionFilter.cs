using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace WeatherForecastByLocation.Server;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is HttpResponseException httpResponseException)
        {
            context.Result = new ObjectResult(httpResponseException.Response.Content.ReadAsStringAsync().GetAwaiter().GetResult())
            {
                StatusCode = (int?)httpResponseException.Response.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }
}