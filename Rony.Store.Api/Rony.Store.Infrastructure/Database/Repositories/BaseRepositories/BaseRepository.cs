using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Exceptions;

namespace Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;
public abstract class BaseRepository<Entity, Id> : IBaseRepository<Entity, Id> where Entity : class
{
    protected readonly DbSet<Entity> _DbSet;
    protected readonly StoreContext _context;

    protected BaseRepository(StoreContext context)
    {
        _context = context;
        _DbSet = _context.Set<Entity>();
    }

    public async Task AddAsync(Entity entity)
    {
        await _DbSet.AddAsync(entity);
    }

    public virtual async Task<Entity> FindById(Id id)
    {
        return await _DbSet.FindAsync(id) ?? throw new EntityNotFoundException($"{EntityName()} with id '{id}' was not found");
    }

    public async Task UpdateAsync(Entity entity)
    {
        (await _DbSet.AddAsync(entity)).State = EntityState.Modified;
    }

    protected abstract string EntityName();

}