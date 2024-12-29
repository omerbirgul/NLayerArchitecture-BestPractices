using AutoMapper;
using Repositories.Entities;
using Services.Products.Dtos;

namespace Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}