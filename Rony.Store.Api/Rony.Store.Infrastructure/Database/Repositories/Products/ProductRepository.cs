using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Exceptions;
using Rony.Store.Domain.Parameters.Products;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Products;
public class ProductRepository(StoreContext context) : BaseRepository<Product, int>(context), IProductRepository
{
    public override async Task<Product> FindById(int id)
    {
        return await _DbSet
             .Include(product => product.Category)
             .FirstOrDefaultAsync(product => product.Id == id)
              ?? throw new EntityNotFoundException($"{EntityName()} with id '{id}' was not found");
    }

    public async Task<List<Product>> FindAsync(FindProductsParameters parameters)
    {
        IQueryable<Product> query = _DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(x => EF.Property<string>(x, "Name").Contains(parameters.Name));

        parameters.Count = await query.CountAsync();

        var results = await query
            .Include(product => product.Category)
            .Skip(parameters.Skip)
            .Take(parameters.PageSize)
            .ToListAsync();

        return results;
    }
    protected override string EntityName() => "Product";
}