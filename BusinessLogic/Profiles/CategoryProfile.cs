using AutoMapper;
using BusinessLogic.DTO.CategoryDTOs;
using Domain.Entities;

namespace BusinessLogic.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryGetDTO, Category>().ReverseMap();
        CreateMap<CategoryPostDTO, Category>().ReverseMap();
        CreateMap<CategoryPutDTO, Category>().ReverseMap();
    }
}