using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Entities.Products;

namespace Rony.Store.Domain.Contracts.Repositories.Products;
public interface IProductRepository : IBaseRepository<Product, int>
{
}
