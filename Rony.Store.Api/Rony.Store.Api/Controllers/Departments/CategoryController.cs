using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;

namespace Rony.Store.Api.Controllers.Departments;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> FindByIdAsync([FromRoute] int id)
    {
        return Ok(await categoryService.FindDTOByIdAsync<CategoryDTO>(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryFormDTO CategoryFormDTO)
    {
        await categoryService.CreateAsync(CategoryFormDTO);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] CategoryFormDTO CategoryFormDTO)
    {
        await categoryService.UpdateByIdAsync(CategoryFormDTO, id);
        return NoContent();
    }

}