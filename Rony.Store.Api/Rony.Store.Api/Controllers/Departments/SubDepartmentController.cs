using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;

namespace Rony.Store.Api.Controllers.Departments;

[ApiController]
[Route("sub-departments")]
public class SubDepartmentController(ISubDepartmentService subDepartmentService) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> FindByIdAsync([FromRoute] int id)
    {
        return Ok(await subDepartmentService.FindDTOByIdAsync<SubDepartmentDTO>(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] SubDepartmentFormDTO SubDepartmentFormDTO)
    {
        await subDepartmentService.CreateAsync(SubDepartmentFormDTO);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] SubDepartmentFormDTO SubDepartmentFormDTO)
    {
        await subDepartmentService.UpdateByIdAsync(SubDepartmentFormDTO, id);
        return NoContent();
    }

}