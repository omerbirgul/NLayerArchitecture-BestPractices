using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ExceptionHandlers;
using Services.Products;

namespace Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}