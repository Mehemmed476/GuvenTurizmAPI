using AutoMapper;
using BusinessLogic.DTO.HouseDTOs;
using Domain.Entities;

namespace BusinessLogic.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<HouseGetDTO, House>().ReverseMap();
        CreateMap<HousePostDTO, House>().ReverseMap();
        CreateMap<HousePutDTO, House>().ReverseMap();
    }
}