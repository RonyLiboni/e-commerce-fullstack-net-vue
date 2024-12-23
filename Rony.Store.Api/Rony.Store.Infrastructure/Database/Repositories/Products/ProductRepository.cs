using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Entities.Products;
using Rony.Store.Domain.Exceptions;
using Rony.Store.Domain.Parameters.Products;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Products;
public class ProductRepository(StoreContext context) : BaseRepository<Product, int>(context), IProductRepository
{
    public override async Task<Product> FindByIdAsync(int id)
    {
        return await _DbSet
             .Include(product => product.Category)
             .FirstOrDefaultAsync(product => product.Id == id)
              ?? throw new EntityNotFoundException($"{EntityName()} with id '{id}' was not found");
    }

    public async Task<List<Product>> FindAsync(FindProductsParameters parameters)
    {
        IQueryable<Product> query = _DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(x => EF.Property<string>(x, "Name").Contains(parameters.Name));

        parameters.Count = await query.CountAsync();

        var results = await query
            .Include(product => product.Category)
            .Skip(parameters.Skip)
            .Take(parameters.PageSize)
            .ToListAsync();

        return results;
    }

    public async Task<List<Product>> FindProductsByCustomerSearchFiltersAsync(FindProductsByCustomerSearchFiltersParameters parameters)
    {
        var query = _DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(x => x.Name.Contains(parameters.Name));

        if (parameters.StartPrice is not null)
            query = query.Where(x => x.Price >= parameters.StartPrice);

        if (parameters.EndPrice is not null)
            query = query.Where(x => x.Price <= parameters.EndPrice);

        if (!parameters.Categories.IsNullOrEmpty())
            query = query.Where(x => parameters.Categories!.Contains(x.Category.Name));

        if (!parameters.SubDepartments.IsNullOrEmpty())
            query = query.Where(x => parameters.SubDepartments!.Contains(x.Category.SubDepartment.Name));

        if (!parameters.Departments.IsNullOrEmpty())
            query = query.Where(x => parameters.Departments!.Contains(x.Category.SubDepartment.Department.Name));

        if (parameters.OrderByDescending)
            query = query.OrderByDescending(x => EF.Property<string>(x, parameters.SortField));
        else
            query = query.OrderBy(x => EF.Property<string>(x, parameters.SortField));

        parameters.Count = await query.CountAsync();

        var results = await query
            .Include(product => product.Category)
            .Skip(parameters.Skip)
            .Take(parameters.PageSize)
            .ToListAsync();

        return results;
    }

    public async Task<List<Department>> FindFiltersByCustomerSearchFiltersAsync(FindFiltersByCustomerSearchFiltersParameters parameters)
    {
        var query = _DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(x => x.Name.Contains(parameters.Name));

        if (parameters.StartPrice is not null)
            query = query.Where(x => x.Price >= parameters.StartPrice);

        if (parameters.EndPrice is not null)
            query = query.Where(x => x.Price <= parameters.EndPrice);


        parameters.Count = await query.CountAsync();

        var results = await query
            .Include(product => product.Category.SubDepartment.Department)
                .ThenInclude(department => department.SubDepartments)
                    .ThenInclude(subdDepartment => subdDepartment.Categories)
            .Select(product => product.Category.SubDepartment.Department)
            .Distinct()
            .Skip(parameters.Skip)
            .Take(parameters.PageSize)
            .ToListAsync();

        return results;
    }
}