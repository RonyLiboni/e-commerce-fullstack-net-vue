using Microsoft.EntityFrameworkCore;
using Rony.Store.Api.Middleware;
using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.Profiles;
using Rony.Store.Domain.Services.Departments;
using Rony.Store.Domain.Services.Products;
using Rony.Store.Infrastructure.Database;
using Rony.Store.Infrastructure.Database.Repositories.Departments;
using Rony.Store.Infrastructure.Database.Repositories.Products;
using Rony.Store.Infrastructure.Database.Repositories.UnityOfWorks;
using Rony.Store.Infrastructure.Storage;

namespace Rony.Store.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddAutoMapper(typeof(DepartmentsProfile).Assembly);
         
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDepartmentService, DepartmentService>();

        services.AddScoped<ISubDepartmentService, SubDepartmentService>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IStorageService, StorageService>();

        services.AddScoped<ICustomerSearchFilterService, CustomerSearchFilterService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        services.AddScoped<ISubDepartmentRepository, SubDepartmentRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

}