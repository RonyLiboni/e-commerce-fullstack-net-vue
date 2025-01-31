using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Exceptions;

namespace Rony.Store.Api.Security;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AuthorizedRolesAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;
    public bool OverrideIfThereIsExistingRolesApplied { get; set; } = false;

    public AuthorizedRolesAttribute(params string[] roles)
    {
        _roles = roles ?? [];
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        ThrowExceptionIfNotAuthenticated(context);
        var roles = GetAllowedRolesForThisEndpoint(context);
        await ThrowExceptionIfUserDoesNotHaveThePermissionsToAccessTheEndpoint(roles, context);
    }

    private static void ThrowExceptionIfNotAuthenticated(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated is true) return;

        throw new SecurityException(401, "You must be authenticated to access this endpoint.");
    }

    private static async Task ThrowExceptionIfUserDoesNotHaveThePermissionsToAccessTheEndpoint(string[] roles, AuthorizationFilterContext context)
    {
        var userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();
        var userEmail = context.HttpContext.User.Identity?.Name;

        if (await userRepository!.DoesUserHaveNeededRolesAsync(userEmail!, roles)) return;

        throw new SecurityException(403, "You do not have the needed permissions to access this endpoint.");
    }

    private string[] GetAllowedRolesForThisEndpoint(AuthorizationFilterContext context)
    {
        var methodAttributes = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizedRolesAttribute>();
        string[] roles = [];

        roles = methodAttributes.FirstOrDefault(a => a.OverrideIfThereIsExistingRolesApplied)?._roles ??
            methodAttributes.FirstOrDefault()?._roles ??
            _roles;
       
        if (roles.IsNullOrEmpty())
        {
            var routeValues = context.ActionDescriptor.RouteValues;
            routeValues.TryGetValue("controller", out var controllerName);
            routeValues.TryGetValue("action", out var methodName);
            roles = [controllerName + '-' + methodName];
        }
        return roles;
    } 
}