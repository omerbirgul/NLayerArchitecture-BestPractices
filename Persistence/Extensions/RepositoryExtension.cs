using Application.Contracts.Persistence;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Categories;
using Persistence.Database;
using Persistence.Interceptors;
using Persistence.Products;

namespace Persistence.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            var connectionStrings =
                configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

            opt.UseSqlServer(connectionStrings!.DefaultConnection, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });

            opt.AddInterceptors(new AuditDbContextInterceptor());
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}