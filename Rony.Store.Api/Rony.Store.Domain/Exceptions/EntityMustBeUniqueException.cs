using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Exceptions;
public class EntityMustBeUniqueException(string? message) : BusinessException(message)
{
    public override async Task HandleContextAsync(HttpContext context)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync(BuildProblemDetail(400, "Client error", "The resource has unique fields.", Message));
    }
}
