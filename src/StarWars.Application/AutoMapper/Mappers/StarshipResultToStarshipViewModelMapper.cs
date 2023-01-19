using AutoMapper;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Infrastructure.HttpAdapters.Starships.Results;

namespace StarWars.Application.AutoMapper.Mappers
{
    public class StarshipResultToStarshipViewModelMapper : Profile
    {
        public StarshipResultToStarshipViewModelMapper()
        {
            CreateMap<StarshipDataResult, StarshipViewModel>();
        }
    }
}
