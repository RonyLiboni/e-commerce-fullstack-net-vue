using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Exceptions;
public class InvalidLoginException(string message) : BusinessException(message)
{
    public override async Task HandleContextAsync(HttpContext context)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync(BuildProblemDetail(401, "Login error", "Login was not successful.", Message));
    }
}
