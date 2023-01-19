using Refit;
using StarWars.Infrastructure.HttpAdapters.Starships.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.HttpAdapters.Starships.Interfaces
{
    public interface IStarshipAdapter
    {
        [Get("/api/starships?page={page}")]
        Task<StarshipResult> GetStarships(int page);
    }
}
