using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Services.BaseServices;

namespace Rony.Store.Domain.Services.Departments;
public class SubDepartmentService(ISubDepartmentRepository subDepartmentRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<SubDepartment, int>(subDepartmentRepository, unitOfWork, mapper), ISubDepartmentService
{
}
