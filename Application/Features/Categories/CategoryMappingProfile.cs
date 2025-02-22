using Application.Features.Categories.Create;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
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