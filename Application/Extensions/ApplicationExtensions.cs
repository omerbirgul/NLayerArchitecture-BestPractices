using System.Reflection;
using Application.Contracts.Caching;
using Application.Features.Categories.Services;
using Application.Features.Products.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
    {
        // validation kendimiz yazmak istediğimiz için .Net default validation özelliğini kapattık.
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
        
        services.AddScoped<IProductService, ProductServiceProxy>();
        services.AddScoped<ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // services.AddExceptionHandler<CriticalExceptionHandler>();
        // services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}