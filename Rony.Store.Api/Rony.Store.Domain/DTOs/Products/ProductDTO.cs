using Rony.Store.Domain.DTOs.BaseDTO;
using Rony.Store.Domain.DTOs.Departments;

namespace Rony.Store.Domain.DTOs.Products;
public class ProductDTO : NameDTO
{
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageKey { get; set; }
    public string ImagePath { get; set; }
    public int CategoryId { get; set; }
    public CategoryDTO Category { get; set; }

}
