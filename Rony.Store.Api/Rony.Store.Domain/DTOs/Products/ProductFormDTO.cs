namespace Rony.Store.Domain.DTOs.Products;
public class ProductFormDTO
{
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageKey { get; set; }
    public string ImagePath { get; set; }
    public int CategoryId { get; set; }
}
