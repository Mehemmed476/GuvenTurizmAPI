using AutoMapper;
using BusinessLogic.DTO.HouseDTOs;
using Domain.Entities;

namespace BusinessLogic.Profiles;

public class HouseFileProfile : Profile
{
    public HouseFileProfile()
    {
        CreateMap<HouseGetDTO, House>().ReverseMap();
        CreateMap<HousePostDTO, House>().ReverseMap();
        CreateMap<HousePutDTO, House>().ReverseMap();
    }
}