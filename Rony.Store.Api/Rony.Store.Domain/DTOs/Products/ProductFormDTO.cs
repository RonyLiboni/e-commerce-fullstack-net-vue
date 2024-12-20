using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.DTOs.Products;
public class ProductFormDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(36)]
    public string Sku { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Description { get; set; }
    [MaxLength(50)]
    public string ImageKey { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(150)]
    public string Name { get; set; }
}
