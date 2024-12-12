using Rony.Store.Domain.Parameters.Pagination;

namespace Rony.Store.Domain.Parameters.Products;
public class FindFiltersByCustomerSearchFiltersParameters : PageParameters
{
    public string? Name { get; set; }
    public decimal? StartPrice { get; set; }
    public decimal? EndPrice { get; set; }
}
