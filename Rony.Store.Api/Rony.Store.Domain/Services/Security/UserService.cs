using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Security;
using Rony.Store.Domain.Entities.Security;
using Rony.Store.Domain.Services.BaseServices;

namespace Rony.Store.Domain.Services.Security;
public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<User, int>(userRepository, unitOfWork, mapper), IUserService
{
    public async Task<List<string>> FindRolesAsync(string? email)
    {
        return await userRepository.GetUserRolesAsync(email);
    }
}
