using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.HttpAdapters.Starships.Results
{
    public class StarshipResult
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<StarshipDataResult> Results { get; set; }
    }
}
