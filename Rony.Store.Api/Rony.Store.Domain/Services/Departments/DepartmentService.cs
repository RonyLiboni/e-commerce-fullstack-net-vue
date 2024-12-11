using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Parameters.Departments;
using Rony.Store.Domain.Parameters.Pagination;
using Rony.Store.Domain.Services.BaseServices;

namespace Rony.Store.Domain.Services.Departments;
public class DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Department, int>(departmentRepository, unitOfWork, mapper), IDepartmentService
{
    public async Task<PagedResult<DepartmentDTO>> FindAsync(FindDepartmentsParameters parameters)
    {
        var departments = await departmentRepository.FindAsync(parameters);
        return new PagedResult<DepartmentDTO>(_mapper.Map<List<DepartmentDTO>>(departments), parameters);
    }
}
