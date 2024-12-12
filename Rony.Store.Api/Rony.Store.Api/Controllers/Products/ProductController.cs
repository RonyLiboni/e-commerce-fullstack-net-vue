using Microsoft.AspNetCore.Mvc;
using Rony.Store.Api.Controllers.BaseController;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Entities.Products;

namespace Rony.Store.Api.Controllers.Products;

[ApiController]
[Route("products")]
public class ProductController(IProductService productService) : BaseCreateReadUpdateController<Product , int, ProductFormDTO, ProductDTO>(productService)
{
}
