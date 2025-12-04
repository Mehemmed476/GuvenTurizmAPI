using AutoMapper;
using BusinessLogic.DTO.BookingDTOs;
using BusinessLogic.DTO.HouseDTOs;
using Domain.Entities;

namespace BusinessLogic.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingGetDTO>().ReverseMap();
        CreateMap<BookingPostDTO, Booking>().ReverseMap();
        CreateMap<BookingPutDTO, Booking>().ReverseMap();
    }
}