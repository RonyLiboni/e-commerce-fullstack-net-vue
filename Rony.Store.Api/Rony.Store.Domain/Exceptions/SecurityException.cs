using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Exceptions;
public class SecurityException(int statusCode, string? message) : BusinessException(message)
{
    public override async Task HandleContextAsync(HttpContext context)
    {
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(BuildProblemDetail(statusCode, "Security error", "You are not authenticated or do not have permissions to access.", Message));
    }
}
