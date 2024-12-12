using Rony.Store.Domain.Contracts.Repositories.BaseRepositories;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Domain.Contracts.Repositories.Departments;
public interface ICategoryRepository : IBaseRepository<Category, int>
{
    Task<List<Category>> FindAll();
}
