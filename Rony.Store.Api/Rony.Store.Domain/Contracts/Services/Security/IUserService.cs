using Rony.Store.Domain.Contracts.Services.BaseServices;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Domain.Contracts.Services.Security;
public interface IUserService : IBaseService<User, int>
{
    Task<List<string>> FindRolesAsync(string? email);
}
