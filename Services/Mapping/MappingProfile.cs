using AutoMapper;
using Repositories.Entities;
using Services.Products.Dtos;
using Services.Products.Dtos.Requests;

namespace Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Name,
                opt => opt
                    .MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.Name,
                opt => opt
                    .MapFrom(src => src.Name.ToLowerInvariant()));
    }
}