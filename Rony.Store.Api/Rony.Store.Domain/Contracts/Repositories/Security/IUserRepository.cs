using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Domain.Contracts.Repositories.Security;
public interface IUserRepository : IBaseRepository<User, int>
{
    Task<bool> DoesUserHaveNeededRolesAsync(string email, string[] roles);
    Task<User?> FindByEmailAsync(string email);
    Task<List<string>> GetUserRolesAsync(string? email);
}
