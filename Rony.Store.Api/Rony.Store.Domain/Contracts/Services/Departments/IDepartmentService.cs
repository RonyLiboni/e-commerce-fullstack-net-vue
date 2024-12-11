using Rony.Store.Domain.Contracts.Services.BaseServices;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Parameters.Departments;
using Rony.Store.Domain.Parameters.Pagination;

namespace Rony.Store.Domain.Contracts.Services.Departments;
public interface IDepartmentService : IBaseService<Department, int>
{
    Task<PagedResult<DepartmentDTO>> FindAsync(FindDepartmentsParameters parameters);
}
