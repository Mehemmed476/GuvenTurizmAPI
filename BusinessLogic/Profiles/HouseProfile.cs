using AutoMapper;
using BusinessLogic.DTO.HouseDTOs;
using Domain.Entities;

namespace BusinessLogic.Profiles;

public class HouseProfile : Profile
{
    public HouseProfile()
    {
        CreateMap<House, HouseGetDTO>();

        CreateMap<HousePostDTO, House>()
            .ForMember(d => d.CoverImage, opt => opt.Ignore())
            .ForMember(d => d.Images, opt => opt.Ignore())
            .ForMember(d => d.HouseHouseAdvantageRels, opt => opt.Ignore());

        CreateMap<HousePutDTO, House>()
            .ForMember(d => d.CoverImage, opt => opt.Ignore())
            .ForMember(d => d.Images, opt => opt.Ignore())
            .ForMember(d => d.HouseHouseAdvantageRels, opt => opt.Ignore());
    }
}
