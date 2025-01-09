using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rony.Store.Api.Middleware;
using Rony.Store.Domain.Contracts.Repositories.Departments;
using Rony.Store.Domain.Contracts.Repositories.Products;
using Rony.Store.Domain.Contracts.Repositories.Security;
using Rony.Store.Domain.Contracts.Repositories.UnitOfWorks;
using Rony.Store.Domain.Contracts.Services.Departments;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
using Rony.Store.Domain.Contracts.Services.Products;
using Rony.Store.Domain.Contracts.Services.Security;
using Rony.Store.Domain.Profiles;
using Rony.Store.Domain.Services.Departments;
using Rony.Store.Domain.Services.Products;
using Rony.Store.Domain.Services.Security;
using Rony.Store.Domain.Settings;
using Rony.Store.Infrastructure.Database;
using Rony.Store.Infrastructure.Database.Repositories.Departments;
using Rony.Store.Infrastructure.Database.Repositories.Products;
using Rony.Store.Infrastructure.Database.Repositories.Security;
using Rony.Store.Infrastructure.Database.Repositories.UnityOfWorks;
using Rony.Store.Infrastructure.Storage;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;

namespace Rony.Store.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddAutoMapper(typeof(DepartmentsProfile).Assembly);

        services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

        services.AddAuthorization();

        services.AddAuthentication(AddJwtScheme())
                .AddJwtBearer(options => AddJwtValidationParameters(options, configuration));

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

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<Domain.Contracts.Services.Security.IAuthenticationService, Domain.Services.Security.AuthenticationService>();

        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        services.AddScoped<ISubDepartmentRepository, SubDepartmentRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

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
        services.AddSwaggerGen(AddSecurityTokenInSwagger());

        return services;
    }

    private static Action<AuthenticationOptions> AddJwtScheme()
    {
        return options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        };
    }

    private static void AddJwtValidationParameters(JwtBearerOptions options, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("TokenSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["AccessTokenSecretKey"]);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            NameClaimType = ClaimTypes.Name
        };
    }

    private static Action<SwaggerGenOptions> AddSecurityTokenInSwagger()
    {
        return options =>
        {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Insert the JWT token in the field below. 'Bearer' will be added automatically."
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        };
    }
}