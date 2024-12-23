using AutoMapper;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.DTOs.Products;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Parameters.Pagination;
using Rony.Store.Domain.Parameters.Products;
using Rony.Store.Domain.Services.BaseServices;

namespace Rony.Store.Domain.Services.Products;
public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, IStorageService storageService) : BaseService<Product, int>(productRepository, unitOfWork, mapper), IProductService
{
    public async Task<PagedResult<ProductDTO>> FindAsync(FindProductsParameters parameters)
    {
        var products = await productRepository.FindAsync(parameters);
        return new PagedResult<ProductDTO>(_mapper.Map<List<ProductDTO>>(products), parameters);
    }

    public async Task CreateAsync(ProductFormDTO entity)
    {
        await base.CreateAsync(entity);
        storageService.MoveFileToLongTermStorage(entity.ImageKey);
    }

    public async Task UpdateByIdAsync(ProductFormDTO form, int id)
    {
        var entity = await FindByIdAsync(id);
        var oldImageKey = entity.ImageKey;
        var imagePathChanged = entity.ImageKey != form.ImageKey;

        await productRepository.UpdateAsync(mapper.Map(form, entity));
        await unitOfWork.SaveChangesAsync();
                
        if (!storageService.IsFileInLongTermStorage(form.ImageKey))
        {
            storageService.MoveFileToLongTermStorage(form.ImageKey);
        }

        if (imagePathChanged)
        {
            storageService.RemoveFile(oldImageKey);
        }
    }
}