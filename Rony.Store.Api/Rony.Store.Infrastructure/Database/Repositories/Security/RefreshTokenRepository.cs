using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Entities.Security;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Security;
public class RefreshTokenRepository(StoreContext context) : BaseCRUDRepository<RefreshToken, Guid>(context), IRefreshTokenRepository
{
    public async Task<bool> DoesRefreshTokenExistByIdAndEmailAsync(Guid id, string email)
    {
        return await _dbSet.AnyAsync(
            refreshToken => refreshToken.Id == id && 
            refreshToken.Email == email &&
            refreshToken.ExpiresAt > DateTime.Now);
    }

    public async Task RemoveAllOldRefreshTokensFromUserAsync(string email, Guid? lastRefreshTokenId)
    {
        var refreshTokensToRemove = await _dbSet
            .Where(refreshToken => refreshToken.Email == email && refreshToken.Id != lastRefreshTokenId)
            .ToListAsync();
        _dbSet.RemoveRange(refreshTokensToRemove);
    }
}
