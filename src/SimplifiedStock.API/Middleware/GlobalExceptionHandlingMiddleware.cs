using SimplifiedStock.Services.Exceptions;
using System.Net;

namespace SimplifiedStock.API.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BusinessException ex)
        {
            await WriteResponse(httpContext, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (NotFoundException ex)
        {
            await WriteResponse(httpContext, HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception)
        {
            await WriteResponse(httpContext, HttpStatusCode.InternalServerError, "Internal server error");
        }
    }

    private async Task WriteResponse(HttpContext httpContext, HttpStatusCode code, string message)
    {
        httpContext.Response.StatusCode = (int)code;
        httpContext.Response.ContentType = "application/json";

        var response = new
        {
            error = message
        };

        await httpContext.Response.WriteAsJsonAsync(response);
    }
}


// Extension method used to add the middleware to the HTTP request pipeline.
public static class GlobalExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
