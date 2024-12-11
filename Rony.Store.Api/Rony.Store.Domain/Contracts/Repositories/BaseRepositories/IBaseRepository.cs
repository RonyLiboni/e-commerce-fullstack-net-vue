namespace Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
public interface IBaseRepository<Entity, Id>
{
    Task<Entity> FindById(Id id);
    Task AddAsync(Entity entity);
    Task UpdateAsync(Entity entity);
}
