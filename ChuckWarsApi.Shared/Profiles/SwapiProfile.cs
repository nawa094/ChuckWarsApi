using AutoMapper;
using ChuckWarsApi.Shared.Dtos.Swapi;
using ChuckWarsApi.Shared.Models.Swapi;

namespace ChuckWarsApi.Shared.Profiles
{
    public class SwapiProfile : Profile
    {
        public SwapiProfile()
        {
            CreateMap<PeopleDto, PeopleModel>();
            CreateMap<PersonDto, PersonModel>()
                .ForMember(dest => dest.BirthYear, opt => opt.MapFrom(src => src.Birth_Year))
                .ForMember(dest => dest.SkinColor, opt => opt.MapFrom(src => src.Skin_Color))
                .ForMember(dest => dest.HairColor, opt => opt.MapFrom(src => src.Hair_Color))
                .ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.Eye_Color));
        }
    }
}
