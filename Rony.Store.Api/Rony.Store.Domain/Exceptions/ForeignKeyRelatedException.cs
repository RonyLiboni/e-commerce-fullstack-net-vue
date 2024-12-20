using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Exceptions;
public class ForeignKeyRelatedExceptionn(string? message) : BusinessException(message)
{
    public override async Task HandleContextAsync(HttpContext context)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync(BuildProblemDetail(400, "Foreign Key error.", "Client sent invalid data.", Message));
    }
}
