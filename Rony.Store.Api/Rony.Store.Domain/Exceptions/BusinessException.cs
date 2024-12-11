using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Rony.Store.Domain.Exceptions;
public abstract class BusinessException(string? message) : Exception(message)
{
    public abstract Task HandleContextAsync(HttpContext context);

    protected static string BuildProblemDetail(int statusCode, string type, string title, string errorMessage)
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