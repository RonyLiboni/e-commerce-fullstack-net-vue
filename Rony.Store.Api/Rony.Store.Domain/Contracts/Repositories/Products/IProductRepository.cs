using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Parameters.Products;

namespace Rony.Store.Domain.Contracts.Repositories.Products;
public interface IProductRepository : IBaseRepository<Product, int>
{
    Task<List<Product>> FindAsync(FindProductsParameters parameters);
}
