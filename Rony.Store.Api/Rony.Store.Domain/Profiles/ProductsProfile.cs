using AutoMapper;
using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Entities.Products;

namespace Rony.Store.Domain.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductFormDTO, Product>();
        CreateMap<Product, ProductDTO>();
    }
}
