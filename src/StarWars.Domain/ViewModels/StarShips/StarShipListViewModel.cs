using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Domain.ViewModels.Starships
{
    public class StarshipListViewModel
    {
        public IList<StarshipViewModel> Starships { get; set; }
    }
}
