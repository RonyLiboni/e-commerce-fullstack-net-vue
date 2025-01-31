using Rony.Store.Domain.Contracts.Services.BaseServices;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Domain.Contracts.Services.Security;
public interface IRefreshTokenService : IBaseCRUDService<RefreshToken, Guid>
{
    Task RemoveAllOldRefreshTokensFromUserAsync(string email, Guid? lastRefreshTokenId = null);
    Task ThrowExceptionIfTokenIsNotValidAsync(Guid refreshTokenId, string email);
}
