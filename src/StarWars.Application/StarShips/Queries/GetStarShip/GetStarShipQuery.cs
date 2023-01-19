using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;

namespace StarWars.Application.Starships.Queries.GetStarshipList
{
    public class GetStarshipQuery : StarshipQuery<List<StarshipViewModel>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
