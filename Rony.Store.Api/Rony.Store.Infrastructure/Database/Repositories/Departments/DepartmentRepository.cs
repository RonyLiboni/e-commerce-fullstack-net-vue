using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Exceptions;
using Rony.Store.Domain.Parameters.Departments;
using Rony.Store.Infrastructure.Database.Repositories.BaseRepositories;

namespace Rony.Store.Infrastructure.Database.Repositories.Departments;
public class DepartmentRepository(StoreContext context) : BaseRepository<Department, int>(context), IDepartmentRepository
{
    public async Task<IList<Department>> FindAsync(FindDepartmentsParameters parameters)
    {
        IQueryable<Department> query = _DbSet.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(parameters.Name))
            query = query.Where(x => EF.Property<string>(x, "Name").Contains(parameters.Name));

        parameters.Count = await query.CountAsync();

        var results = await query
            .Include(department => department.SubDepartments)
                .ThenInclude(subDepartment => subDepartment.Categories)
            .Skip(parameters.Skip)
            .Take(parameters.PageSize)
            .ToListAsync();

        return results;
    }

    public override async Task<Department> FindById(int id)
    {
       return await _DbSet
            .Include(department => department.SubDepartments)
                .ThenInclude(subDepartment => subDepartment.Categories)
            .FirstOrDefaultAsync(department => department.Id == id)
             ?? throw new EntityNotFoundException($"{EntityName()} with id '{id}' was not found");
    }

    protected override string EntityName() => "Department";
}
