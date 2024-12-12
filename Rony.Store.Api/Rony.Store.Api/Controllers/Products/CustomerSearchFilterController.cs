using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.Parameters.Products;

namespace Rony.Store.Api.Controllers.Products;

[ApiController]
[Route("customer-search-filters")]
public class CustomerSearchFilterController(ICustomerSearchFilterService customerSearchFilterService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> FindProductsByCustomerSearchFiltersAsync([FromQuery] FindProductsByCustomerSearchFiltersParameters parameters)
    {
        return Ok(await customerSearchFilterService.FindProductsByCustomerSearchFiltersAsync(parameters));
    }

    [HttpGet("filters")]
    public async Task<IActionResult> FindFiltersByCustomerSearchFiltersAsync([FromQuery] FindFiltersByCustomerSearchFiltersParameters parameters)
    {
        return Ok(await customerSearchFilterService.FindFiltersByCustomerSearchFiltersAsync(parameters));
    }
}
