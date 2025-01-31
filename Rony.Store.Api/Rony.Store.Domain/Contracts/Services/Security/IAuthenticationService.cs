using Microsoft.AspNetCore.Http;
using Rony.Store.Domain.DTOs.Login;

namespace Rony.Store.Domain.Contracts.Services.Security;
public interface IAuthenticationService
{
    Task<LoginDTO> AuthenticateAsync(LoginFormDTO form);
    Task LogoutAsync(string? refreshToken);
    Task<LoginDTO> RefreshLoginAsync(string? refreshToken);
}
