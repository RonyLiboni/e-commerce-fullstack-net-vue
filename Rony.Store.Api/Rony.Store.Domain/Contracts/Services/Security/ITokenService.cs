using Rony.Store.Domain.DTOs.Login;
using System.Security.Claims;

namespace Rony.Store.Domain.Contracts.Services.Security;
public interface ITokenService
{
    Task<LoginDTO> GenerateLoginTokensAsync(string email);
    ClaimsPrincipal GetClaimsFromRefreshToken(string? jwtRefreshToken);
    string GetEmailFromRefreshToken(string jwtRefreshToken);
}
