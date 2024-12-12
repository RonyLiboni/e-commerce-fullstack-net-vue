using Microsoft.AspNetCore.Mvc;
using Rony.Store.Api.Controllers.BaseController;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Parameters.Departments;
namespace Rony.Store.Api.Controllers.Departments;

[ApiController]
[Route("departments")]
public class DepartmentController(IDepartmentService departmentService) : BaseCreateReadUpdateController<Department, int, DepartmentFormDTO, DepartmentDTO>(departmentService)
{
    [HttpGet]
    public async Task<IActionResult> FindAsync([FromQuery] FindDepartmentsParameters parameters)
    {
        return Ok(await departmentService.FindAsync(parameters));
    }
}