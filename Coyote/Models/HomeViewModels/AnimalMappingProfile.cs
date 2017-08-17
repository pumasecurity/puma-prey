using AutoMapper;

using Puma.Prey.Rabbit.Models;

namespace Coyote.Models.HomeViewModels
{
    public class AnimalMappingProfile : Profile
    {
        public AnimalMappingProfile()
        {
            CreateMap<Animal, AnimalViewModel>()
                .ReverseMap();
        }
    }
}