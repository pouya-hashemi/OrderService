using System.Net;
using Beta.OrderService.Domain.Exceptions;
using Newtonsoft.Json;

namespace Beta.OrderService.WebApi.Common;

public class ExceptionHandlerMiddleWare
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BadRequestException badEx)
        {
            context.Response.StatusCode = (int) badEx.Data["status"];
            var body = JsonConvert.SerializeObject(new {ErrorMessage = badEx.Message});
            await context.Response.WriteAsync(body);
        }
        catch (NotFoundException badEx)
        {
            context.Response.StatusCode = (int) badEx.Data["status"];
            var body = JsonConvert.SerializeObject(new {ErrorMessage = badEx.Message});
            await context.Response.WriteAsync(body);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            var body = JsonConvert.SerializeObject(new {ErrorMessage = "Somthing Went Wrong"});
            await context.Response.WriteAsync(body);

            throw;
        }
    }
}