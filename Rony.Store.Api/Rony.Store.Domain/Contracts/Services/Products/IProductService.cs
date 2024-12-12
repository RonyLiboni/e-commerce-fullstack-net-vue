using Rony.Store.Domain.Contracts.Services.BaseServices;
using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Parameters.Pagination;
using Rony.Store.Domain.Parameters.Products;

namespace Rony.Store.Domain.Contracts.Services.Products;
public interface IProductService : IBaseService<Product, int>
{
    Task<PagedResult<ProductDTO>> FindAsync(FindProductsParameters parameters);
}
