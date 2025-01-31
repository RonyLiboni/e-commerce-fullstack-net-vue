using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.BaseServices;

namespace Rony.Store.Domain.Services.BaseServices;
public abstract class BaseCRUDService<Entity, Id>(IBaseCRUDRepository<Entity, Id> baseCRUDRepository, IUnitOfWork unitOfWork, IMapper mapper) 
    : BaseService<Entity, Id>(baseCRUDRepository, unitOfWork, mapper), IBaseCRUDService<Entity, Id> where Entity : class
{
    public async Task RemoveByIdAsync(Id id)
    {
        await baseCRUDRepository.RemoveByIdAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}
