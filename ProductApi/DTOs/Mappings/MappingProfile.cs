using AutoMapper;
using ProductApi.Models;

namespace ProductApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<ProductDto, Product>();
        
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));
    }
}