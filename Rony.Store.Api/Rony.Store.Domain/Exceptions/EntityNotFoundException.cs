using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Exceptions;
public class EntityNotFoundException(string? message) : BusinessException(message)
{
    public override async Task HandleContextAsync(HttpContext context)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync(BuildProblemDetail(404, "Client error", "Entity was not found.", Message));
    }
}
