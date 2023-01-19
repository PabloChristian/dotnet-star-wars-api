using Refit;
using StarWars.Infrastructure.HttpAdapters.StarShips.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.HttpAdapters.StarShips.Interfaces
{
    public interface IStarShipAdapter
    {
        [Get("/api/starships")]
        Task<StarShipResult> GetStarShips();
    }
}
