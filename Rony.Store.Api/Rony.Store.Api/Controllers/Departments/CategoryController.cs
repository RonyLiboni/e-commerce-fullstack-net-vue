using Microsoft.AspNetCore.Mvc;
using Rony.Store.Api.Controllers.BaseController;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.DTOs.Departments;
using Rony.Store.Domain.Entities.Departments;
namespace Rony.Store.Api.Controllers.Departments;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService categoryService) : BaseCreateReadUpdateController<Category, int, CategoryFormDTO, CategoryDTO>(categoryService)
{
    
    [HttpGet]
    public async Task<IActionResult> FindAllAsync()
    {
        return Ok(await categoryService.FindAllAsync());
    }
}