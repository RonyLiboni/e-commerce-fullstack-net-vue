using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.BaseServices;

namespace Rony.Store.Domain.Services.BaseServices;
public abstract class BaseService<Entity, Id> : IBaseService<Entity, Id> where Entity : class
{
    private readonly IBaseRepository<Entity, Id> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected BaseService(IBaseRepository<Entity, Id> repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateAsync<EntityForm>(EntityForm modelToCreate)
    {
        await _repository.AddAsync(_mapper.Map<Entity>(modelToCreate));
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<Entity> FindByIdAsync(Id Id)
    {
        return await _repository.FindById(Id);
    }

    public async Task UpdateByIdAsync<EntityForm>(EntityForm entityForm, Id id)
    {
        _mapper.Map(entityForm, await FindByIdAsync(id));
        await _unitOfWork.SaveChangesAsync();
    }

}
