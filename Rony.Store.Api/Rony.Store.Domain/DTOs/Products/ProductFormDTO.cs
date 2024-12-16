using Rony.Store.Domain.DTOs.BaseDTO;
using System.Text.Json.Serialization;

namespace Rony.Store.Domain.DTOs.Products;
public class ProductFormDTO : NameDTO
{
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageKey { get; set; }
    public int CategoryId { get; set; }
}
