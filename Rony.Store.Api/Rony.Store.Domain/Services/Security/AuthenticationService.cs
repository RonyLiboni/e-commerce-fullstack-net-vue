using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Contracts.Services.Security;
using Rony.Store.Domain.DTOs.Login;
using Rony.Store.Domain.Entities.Security;
using Rony.Store.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Rony.Store.Domain.Services.Security;
public class AuthenticationService(IUserRepository userRepository, ITokenService tokenService, IRefreshTokenService refreshTokenService) : IAuthenticationService
{
    public async Task<LoginDTO> AuthenticateAsync(LoginFormDTO form)
    {
        var user = await userRepository.FindByEmailAsync(form.Email);
        if (PasswordsAreEqual(form, user))
        {
            return await tokenService.GenerateLoginTokensAsync(form.Email);
        }
        throw new InvalidLoginException("Username or password provided are invalid.");
    }

    public async Task<LoginDTO> RefreshLoginAsync(string? jwtRefreshToken)
    {
        try
        {
            var claims = tokenService.GetClaimsFromRefreshToken(jwtRefreshToken);
            var refreshTokenId = Guid.Parse(claims.FindFirst(JwtRegisteredClaimNames.Jti)!.Value);
            var email = claims.FindFirst(ClaimTypes.Name)!.Value;
            await refreshTokenService.ThrowExceptionIfTokenIsNotValidAsync(refreshTokenId, email);
            return await tokenService.GenerateLoginTokensAsync(email);
        }
        catch (Exception ex) when (ex is EntityNotFoundException || ex is FormatException)
        {
            throw new InvalidLoginException("Refresh token provided is invalid.");
        }
    }

    public async Task LogoutAsync(string? jwtRefreshToken)
    {
        if (string.IsNullOrEmpty(jwtRefreshToken)) return;

        try
        {
            var email = tokenService.GetEmailFromRefreshToken(jwtRefreshToken);
            await refreshTokenService.RemoveAllOldRefreshTokensFromUserAsync(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception happenned in LogoutAsync. Message: {ex.Message}");
        }
    }

    private static bool PasswordsAreEqual(LoginFormDTO form, User? user)
    {
        return user is not null && BCrypt.Net.BCrypt.Verify(form.Password, user.Password);
    }
}