using Rony.Store.Domain.Parameters.Pagination;

namespace Rony.Store.Domain.Parameters.Departments;
public class FindDepartmentsParameters : PageParameters
{
    public string? Name { get; set; }
}
