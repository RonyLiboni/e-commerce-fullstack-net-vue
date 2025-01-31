using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Security;
using Rony.Store.Domain.DTOs.Login;
using Rony.Store.Domain.Entities.Security;
using Rony.Store.Domain.Exceptions;
using Rony.Store.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rony.Store.Domain.Services.Security;
public class TokenService(IOptions<TokenSettings> jwtSettingsOptions, IRefreshTokenService refreshTokenService, IUnitOfWork unitOfWork) : ITokenService
{
    private readonly TokenSettings _jwtSettings = jwtSettingsOptions.Value;

    public async Task<LoginDTO> GenerateLoginTokensAsync(string email)
    {
        return new LoginDTO { AccessToken = GenerateJwtAccessToken(email), 
                              RefreshToken = await GenerateJwtRefreshToken(email),
                              RefreshTokenExpiresAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDurationInDays)
                            };
    }

    public ClaimsPrincipal GetClaimsFromRefreshToken(string? token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            throw new InvalidLoginException("Refresh token provided is invalid.");
        }
    }

    public string GetEmailFromRefreshToken(string? token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.RefreshTokenSecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        return tokenHandler.ValidateToken(token, validationParameters, out _).FindFirst(ClaimTypes.Name)!.Value;
    }

    private async Task<string> GenerateJwtRefreshToken(string email)
    {
        var refreshToken = await AddRefreshTokenAsync(email);
        var jwtRefreshToken = BuildJwtRefreshToken(refreshToken);
        await refreshTokenService.RemoveAllOldRefreshTokensFromUserAsync(email, refreshToken.Id);
        return jwtRefreshToken;
    }

    private async Task<RefreshToken> AddRefreshTokenAsync(string email)
    {
        var refreshTokenId = Guid.NewGuid();
        var expiresAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenDurationInDays);
        var refreshToken = new RefreshToken(refreshTokenId, email, expiresAt);
        await refreshTokenService.CreateAsync(refreshToken);
        await unitOfWork.SaveChangesAsync();

        return refreshToken;
    }

    private string BuildJwtRefreshToken(RefreshToken refreshToken)
    {
        return GenerateJwtToken(refreshToken.Email, refreshToken.ExpiresAt, GenerateCredentials(_jwtSettings.RefreshTokenSecretKey), refreshToken.Id.ToString());
    }

    private static SigningCredentials GenerateCredentials(string secretKey)
    {
        var key = Encoding.UTF8.GetBytes(secretKey);
        return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    }

    private string GenerateJwtAccessToken(string username)
    {
        var expiresAt = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenDurationInMinutes);
        return GenerateJwtToken(username, expiresAt, GenerateCredentials(_jwtSettings.AccessTokenSecretKey), Guid.NewGuid().ToString());
    }

    private string GenerateJwtToken(string username, DateTime expiresAt, SigningCredentials credentials, string jti)
    {
        var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, jti)
            };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
