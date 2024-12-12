using Rony.Store.Domain.Entities.BaseEntities;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Domain.Entities.Products;
public class Product : BaseEntityWithName
{
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ImageKey { get; set; }
    public string ImagePath { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
