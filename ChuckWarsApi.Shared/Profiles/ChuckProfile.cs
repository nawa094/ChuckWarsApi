using AutoMapper;
using ChuckWarsApi.Shared.Dtos.Chuck;
using ChuckWarsApi.Shared.Models.Chuck;

namespace ChuckWarsWebAssembly.Shared.Profiles
{
    public class ChuckProfile : Profile
    {
        public ChuckProfile()
        {
            CreateMap<JokeDto, ChuckJokeModel>().ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Icon_Url));
            CreateMap<CategoriesDto, ChuckCategoriesModel>();
            CreateMap<SearchDto, ChuckSearchModel>();
        }
    }
}
