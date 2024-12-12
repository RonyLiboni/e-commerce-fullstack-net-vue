using Rony.Store.Domain.Contracts.Services.BaseServices;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Domain.Contracts.Services.Departments;
public interface ICategoryService : IBaseService<Category, int>
{
    Task<List<CategoryDTO>> FindAllAsync();
}