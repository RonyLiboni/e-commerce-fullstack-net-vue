using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Services.BaseServices;

namespace Rony.Store.Domain.Services.Products;
public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : BaseService<Product, int>(productRepository, unitOfWork, mapper), IProductService
{
}