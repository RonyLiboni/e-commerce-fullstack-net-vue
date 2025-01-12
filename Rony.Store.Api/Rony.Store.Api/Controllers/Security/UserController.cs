using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Security;

namespace Rony.Store.Api.Controllers.Security;

[ApiController]
[Route("users")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("roles")]
    [Authorize]
    public async Task<IActionResult> FindRolesAsync()
    {
        return Ok(await userService.FindRolesAsync(User.Identity?.Name));
    }
}
