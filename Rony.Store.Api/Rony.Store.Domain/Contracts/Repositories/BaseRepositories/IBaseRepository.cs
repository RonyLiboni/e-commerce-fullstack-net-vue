namespace Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
public interface IBaseRepository<Entity, Id>
{
    Task<Entity> FindByIdAsync(Id id);
    Task AddAsync(Entity entity);
    Task UpdateAsync(Entity entity);
}
