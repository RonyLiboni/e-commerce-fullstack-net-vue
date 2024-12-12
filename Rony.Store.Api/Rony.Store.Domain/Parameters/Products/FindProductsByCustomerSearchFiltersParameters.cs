using Rony.Store.Domain.Parameters.Pagination;

namespace Rony.Store.Domain.Parameters.Products;
public class FindProductsByCustomerSearchFiltersParameters : PageParameters
{
    public string? Name { get; set; }
    public decimal? StartPrice { get; set; }
    public decimal? EndPrice { get; set; }
    public List<string>? Departments {  get; set; }
    public List<string>? SubDepartments { get; set; }
    public List<string>? Categories { get; set; }
    public string SortField { get; set; } = "Id";
    public bool OrderByDescending { get; set; } = false;
}
