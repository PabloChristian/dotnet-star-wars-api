using StarWars.Application.StarShips.Query;
using StarWars.Domain.ViewModels.StarShips;

namespace StarWars.Application.StarShips.Queries.GetStarShipList
{
    public class GetStarShipListQuery : StarShipQuery<StarShipListViewModel>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
