using FluentValidation;
using StarWars.Application.Starships.Query;
using StarWars.Domain.ViewModels.Starships;
using StarWars.Shared.Kernel.Handler;

namespace StarWars.Application.Starships.Queries.GetStarshipList
{
    public class GetStarshipQuery : StarshipQuery<List<StarshipViewModel>>
    {
        public GetStarshipQuery(int page = 1, string manufacturer = "")
        {
            Page = page;
            Manufacturer = manufacturer;
        }
    }
}
