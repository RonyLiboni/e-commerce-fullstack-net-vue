using Microsoft.AspNetCore.Mvc;
using Rony.Store.Api.Controllers.BaseController;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Parameters.Products;

namespace Rony.Store.Api.Controllers.Products;

[ApiController]
[Route("products")]
public class ProductController(IProductService productService) : BaseCreateReadUpdateController<Product , int, ProductFormDTO, ProductDTO>(productService)
{
    [HttpGet]
    public async Task<IActionResult> FindAsync([FromQuery] FindProductsParameters parameters)
    {
        return Ok(await productService.FindAsync(parameters));
    }

    public override async Task<IActionResult> CreateAsync([FromBody] ProductFormDTO formDTO)
    {
        await productService.CreateAsync(formDTO);
        return Created();
    }

    public override async Task<IActionResult> UpdateByIdAsync([FromRoute] int id, [FromBody] ProductFormDTO formDTO)
    {
        await productService.UpdateByIdAsync(formDTO, id);
        return Ok("Entity successfully updated.");
    }
}
