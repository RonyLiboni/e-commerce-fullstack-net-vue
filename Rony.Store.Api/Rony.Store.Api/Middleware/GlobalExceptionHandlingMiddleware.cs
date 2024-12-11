using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Exceptions;
using System.Text.Json;

namespace Rony.Store.Api.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";
            if (e is BusinessException businessException)
            {
                _ = businessException.HandleContextAsync(context);
                return;
            }

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(
                BuildProblemDetail(500, "Server error",
                    "Something went wrong. Contact an administrator or wait a few minutes and try again.", e.Message));
        }
    }

    public static string BuildProblemDetail(int statusCode, string type, string title, string errorMessage)
    {
        return JsonSerializer.Serialize(new ProblemDetails()
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = errorMessage
        });
    }
}
