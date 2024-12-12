using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Exceptions;
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
    protected override string EntityName() => "Product";
}