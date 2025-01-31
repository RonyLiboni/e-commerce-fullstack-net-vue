using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Security;
using Rony.Store.Domain.DTOs.Login;

namespace Rony.Store.Api.Controllers.Login;

[ApiController]
[Route("auth")]
public class AuthenticationController(IAuthenticationService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginFormDTO form)
    {
        var loginDTO = await authService.AuthenticateAsync(form);
        UpdateRefreshTokenCookie(loginDTO);
        return Ok(loginDTO);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshLoginAsync()
    {
        var loginDTO = await authService.RefreshLoginAsync(Request.Cookies["refreshToken"]);
        UpdateRefreshTokenCookie(loginDTO);
        return Ok(loginDTO);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await authService.LogoutAsync(Request.Cookies["refreshToken"]);
        UpdateRefreshTokenCookie(null);
        return Ok();
    }

    private void UpdateRefreshTokenCookie(LoginDTO? loginDTO)
    {
        string refreshToken = loginDTO?.RefreshToken ?? string.Empty;
        DateTime expiresAt = loginDTO?.RefreshTokenExpiresAt ?? DateTime.Now.AddDays(-1);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expiresAt,
            Path = "/auth/"
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
