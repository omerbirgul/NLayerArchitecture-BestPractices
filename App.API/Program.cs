using Microsoft.AspNetCore.Mvc;
using Repositories.Extensions;
using Services;
using Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
// validation kendimiz yazmak istediğimiz için .Net default validation özelliğini kapattık.

builder.Services.AddRepositories(builder.Configuration).AddService(builder.Configuration);
// Repository ve Service içinde yazdığımız extension class'ları kullanmak için bu kodu kullandık.


// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<FluentValidationFilter>();
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();