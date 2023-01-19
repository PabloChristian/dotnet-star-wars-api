using MediatR;
using StarWars.Application.StarShips.Query;
using StarWars.Domain.ViewModels.StarShips;

namespace StarWars.Application.StarShips.Queries.GetStarShipByUser
{
    public class GetStarShipByUserQuery : StarShipQuery<StarShipListViewModel>
    {
        public string Manufacturer { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
