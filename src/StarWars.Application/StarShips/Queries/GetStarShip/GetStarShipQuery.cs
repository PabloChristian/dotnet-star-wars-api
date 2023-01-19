using FluentValidation;
using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;

namespace StarWars.Application.Starships.Queries.GetStarshipList
{
    public class GetStarshipQuery : StarshipQuery<List<StarshipViewModel>>
    {
    }
}
