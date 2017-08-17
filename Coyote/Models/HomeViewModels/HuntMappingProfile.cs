using AutoMapper;

using Puma.Prey.Rabbit.Models;

namespace Coyote.Models.HomeViewModels
{
    public class HuntMappingProfile : Profile
    {
        public HuntMappingProfile()
        {
            CreateMap<Hunt, HuntViewModel>()
                .ReverseMap();
        }
    }
}