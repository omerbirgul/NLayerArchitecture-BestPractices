using AutoMapper;
using Repositories.Entities;
using Services.Categories.Dtos;
using Services.Categories.Dtos.Create;
using Services.Categories.Dtos.Update;

namespace Services.Categories.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<CategoryWithProductsDto, Category>().ReverseMap();
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Name,
                opt => opt
                    .MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest => dest.Name,
                opt => opt
                    .MapFrom(src => src.Name.ToLowerInvariant()));
    }
}