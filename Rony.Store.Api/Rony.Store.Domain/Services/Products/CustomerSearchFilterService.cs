using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Parameters.Pagination;
using Rony.Store.Domain.Parameters.Products;

namespace Rony.Store.Domain.Services.Products;
public class CustomerSearchFilterService(IProductRepository productRepository, IMapper mapper) : ICustomerSearchFilterService
{
    public async Task<CustomerSearchFilterDTO> FindFiltersByCustomerSearchFiltersAsync(FindFiltersByCustomerSearchFiltersParameters parameters)
    {
        var filters= await productRepository.FindFiltersByCustomerSearchFiltersAsync(parameters);

        return new CustomerSearchFilterDTO(filters);
    }

    public async Task<PagedResult<ProductDTO>> FindProductsByCustomerSearchFiltersAsync(FindProductsByCustomerSearchFiltersParameters parameters)
    {
        var products = await productRepository.FindProductsByCustomerSearchFiltersAsync(parameters);

        return new PagedResult<ProductDTO>(mapper.Map<List<ProductDTO>>(products), parameters);
    }
}