using FluentValidation;
using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Shared.Kernel.Handler;

namespace StarWars.Application.Starships.Queries.GetStarshipByManufacture
{
    public class GetStarshipByManufacturerQuery : StarshipQuery<List<StarshipViewModel>>
    {
        public string Manufacturer { get; set; }
    }
}
