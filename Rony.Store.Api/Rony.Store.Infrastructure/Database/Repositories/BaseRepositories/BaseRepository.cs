using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Exceptions;

namespace Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;
public abstract class BaseRepository<Entity, Id> : IBaseRepository<Entity, Id> where Entity : class
{
    protected readonly DbSet<Entity> _dbSet;
    protected readonly StoreContext _context;

    protected BaseRepository(StoreContext context)
    {
        _context = context;
        _dbSet = _context.Set<Entity>();
    }

    public async Task AddAsync(Entity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task<Entity> FindByIdAsync(Id id)
    {
        return await _dbSet.FindAsync(id) ?? throw new EntityNotFoundException($"{EntityName()} with id '{id}' was not found");
    }

    public async Task UpdateAsync(Entity entity)
    {
        (await _dbSet.AddAsync(entity)).State = EntityState.Modified;
    }

    protected virtual string EntityName() => typeof(Entity).Name;

}