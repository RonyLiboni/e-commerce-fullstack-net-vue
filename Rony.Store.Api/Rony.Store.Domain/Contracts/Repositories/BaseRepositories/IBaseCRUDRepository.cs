namespace Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
public interface IBaseCRUDRepository<Entity, Id> : IBaseRepository<Entity, Id>
{
    Task RemoveByIdAsync(Id id);
}