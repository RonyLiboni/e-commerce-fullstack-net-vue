using Microsoft.AspNetCore.Mvc;
using Rony.Store.Api.Controllers.BaseController;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;
namespace Rony.Store.Api.Controllers.Departments;

[ApiController]
[Route("sub-departments")]
public class SubDepartmentController(ISubDepartmentService subDepartmentService) : BaseCreateReadUpdateController<SubDepartment, int, SubDepartmentFormDTO, SubDepartmentDTO>(subDepartmentService)
{ }