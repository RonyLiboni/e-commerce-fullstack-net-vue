using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Entities.Security;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Security;
public class UserRepository(StoreContext context) : BaseRepository<User, int>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<List<string>> GetUserRolesAsync(string? email)
    {
        return await _dbSet
            .Where(user => user.Email == email)
            .SelectMany(user => user.GroupRoles.SelectMany(groupRole => groupRole.Roles))
            .Select(role => role.Name)
            .ToListAsync();
    }

    public async Task<bool> DoesUserHaveNeededRolesAsync(string email, string[] roles)
    {
        return await _dbSet
            .Where(user => user.Email == email)
            .SelectMany(user => user.GroupRoles.SelectMany(groupRole => groupRole.Roles))
            .AnyAsync(role => roles.Contains(role.Name));
    }
}
