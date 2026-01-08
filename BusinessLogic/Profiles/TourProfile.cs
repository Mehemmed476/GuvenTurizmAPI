using AutoMapper;
using BusinessLogic.DTO.TourDTOs;
using BusinessLogic.DTO.TourPackageDTOs;
using BusinessLogic.DTO.TourPackageInclusionDTOs;
using Domain.Entities;

namespace BusinessLogic.Profiles
{
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            // --- TOUR ---
            CreateMap<Tour, TourGetDTO>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.TourFiles.Select(x => x.Path).ToList()));

            CreateMap<TourPostDTO, Tour>()
                .ForMember(dest => dest.TourFiles, opt => opt.Ignore()); 

            CreateMap<TourPutDTO, Tour>();

            // --- PACKAGE ---
            CreateMap<TourPackage, TourPackageGetDTO>();
            
            CreateMap<TourPackagePostDTO, TourPackage>()
                .ForMember(dest => dest.Inclusions, opt => opt.MapFrom(src => src.Inclusions.Select(i => new TourPackageInclusion { Description = i })));

            CreateMap<TourPackagePutDTO, TourPackage>();

            // --- INCLUSION ---
            CreateMap<TourPackageInclusion, TourPackageInclusionGetDTO>();
        }
    }
}