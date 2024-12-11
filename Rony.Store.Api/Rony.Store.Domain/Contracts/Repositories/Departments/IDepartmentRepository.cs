using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Parameters.Departments;

namespace Rony.Store.Domain.Contracts.Repositories.Departments;
public interface IDepartmentRepository : IBaseRepository<Department, int>
{
    Task<IList<Department>> FindAsync(FindDepartmentsParameters parameters);
}
