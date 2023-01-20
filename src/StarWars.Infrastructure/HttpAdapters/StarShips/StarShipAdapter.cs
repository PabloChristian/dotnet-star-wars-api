using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using StarWars.Domain.Exceptions;
using StarWars.Infrastructure.HttpAdapters.Starships.Interfaces;
using StarWars.Infrastructure.HttpAdapters.Starships.Results;
using System.Linq;

namespace StarWars.Infrastructure.HttpAdapters.Starships
{
    public class StarshipAdapter
    {
        private readonly IStarshipAdapter _starShip;

        public StarshipAdapter(IStarshipAdapter starShip)
        {
            _starShip = starShip;
        }

        public async Task<List<StarshipDataResult>> GetStarships(int page)
        {
            try
            {
                var starShips = await _starShip.GetStarships(page);

                return starShips?.Results == null
                    ? throw new IntegrationException("Starship integration error: No response from Star Wars API")
                    : starShips.Results;
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Starship integration error: {ex.Message}");
                throw;
            }
        }
    }
}
