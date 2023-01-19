using Refit;
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

        public async Task<List<StarshipDataResult>> GetStarships()
        {
            try
            {
                var starShips = await _starShip.GetStarships();

                return starShips?.Results == null
                    ? throw new Exception("Starship integration error: No response from Star Wars API")
                    : starShips.Results;
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Starship integration error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<StarshipDataResult>> GetStarshipsByManufacturer(string manufacturer)
        {
            try
            {
                var starShips = await GetStarships();
                return starShips.Where(
                    x => x.Manufacturer.Equals(manufacturer,StringComparison.InvariantCultureIgnoreCase)
                ).ToList();
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"Starship integration error: {ex.Message}");
                throw;
            }
        }
    }
}
