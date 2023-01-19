using Refit;
using StarWars.Infrastructure.HttpAdapters.StarShips.Interfaces;
using StarWars.Infrastructure.HttpAdapters.StarShips.Results;

namespace StarWars.Infrastructure.HttpAdapters.StarShips
{
    public class StarShipAdapter
    {
        private readonly IStarShipAdapter _starShip;

        public StarShipAdapter(IStarShipAdapter starShip)
        {
            _starShip = starShip;
        }

        public async Task<List<StarShipDataResult>> GetStarShips()
        {
            try
            {
                var starShips = await _starShip.GetStarShips();

                return starShips?.Results == null
                    ? throw new Exception("StarShip integration error: No response from Star Wars API")
                    : starShips.Results;
            }
            catch (ApiException ex)
            {
                Console.WriteLine($"StarShip integration error: {ex.Message}");
                throw;
            }
        }
    }
}
