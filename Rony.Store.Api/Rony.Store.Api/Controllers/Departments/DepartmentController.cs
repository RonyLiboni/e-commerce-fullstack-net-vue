using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Parameters.Departments;

namespace Rony.Store.Api.Controllers.Departments;

[ApiController]
[Route("departments")]
public class DepartmentController(IDepartmentService departmentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> FindAsync([FromQuery] FindDepartmentsParameters parameters)
    {
        return Ok(await departmentService.FindAsync(parameters));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindByIdAsync([FromRoute] int id)
    {
        return Ok(await departmentService.FindDTOByIdAsync<DepartmentDTO>(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DepartmentFormDTO departmentFormDTO)
    {
        await departmentService.CreateAsync(departmentFormDTO);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] DepartmentFormDTO departmentFormDTO)
    {
        await departmentService.UpdateByIdAsync(departmentFormDTO, id);
        return NoContent();
    }

}