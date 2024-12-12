using Rony.Store.Domain.Contracts.Services.BaseServices;
using Rony.Store.Domain.Entities.Products;

namespace Rony.Store.Domain.Contracts.Services.Products;
public interface IProductService : IBaseService<Product, int>
{
}
