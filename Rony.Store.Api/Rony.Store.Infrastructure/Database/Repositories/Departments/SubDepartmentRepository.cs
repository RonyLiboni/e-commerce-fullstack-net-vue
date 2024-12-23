using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Departments;
public class SubDepartmentRepository(StoreContext context) : BaseRepository<SubDepartment, int>(context), ISubDepartmentRepository
{
}