using MediatR;
using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;

namespace StarWars.Application.Starships.Queries.GetStarshipByManufacture
{
    public class GetStarshipByManufacturerQuery : StarshipQuery<List<StarshipViewModel>>
    {
        public string Manufacturer { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
