using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;
public class BaseCRUDRepository<Entity, Id>(StoreContext context) : BaseRepository<Entity, Id>(context), IBaseCRUDRepository<Entity, Id> where Entity : class
{
    public async Task RemoveByIdAsync(Id id)
    {
        _dbSet.Remove(await FindByIdAsync(id));
    }
}
