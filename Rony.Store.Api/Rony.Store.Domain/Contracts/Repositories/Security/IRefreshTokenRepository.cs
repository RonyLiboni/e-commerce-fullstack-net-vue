using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Domain.Contracts.Repositories.Security;
public interface IRefreshTokenRepository : IBaseCRUDRepository<RefreshToken, Guid>
{
    Task<bool> DoesRefreshTokenExistByIdAndEmailAsync(Guid id, string email);
    Task RemoveAllOldRefreshTokensFromUserAsync(string email, Guid? lastRefreshTokenId);
}
