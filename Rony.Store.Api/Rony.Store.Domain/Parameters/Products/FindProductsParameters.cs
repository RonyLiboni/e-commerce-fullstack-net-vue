using Rony.Store.Domain.Parameters.Pagination;

namespace Rony.Store.Domain.Parameters.Products;
public class FindProductsParameters : PageParameters
{
    public string? Name { get; set; }
}