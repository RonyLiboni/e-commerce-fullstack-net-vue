using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Parameters.Pagination;
using Rony.Store.Domain.Parameters.Products;

namespace Rony.Store.Domain.Contracts.Services.Products;
public interface ICustomerSearchFilterService
{
    Task<CustomerSearchFilterDTO> FindFiltersByCustomerSearchFiltersAsync(FindFiltersByCustomerSearchFiltersParameters parameters);
    Task<PagedResult<ProductDTO>> FindProductsByCustomerSearchFiltersAsync(FindProductsByCustomerSearchFiltersParameters parameters);
}
