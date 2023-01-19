using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars.Domain.ViewModels.StarShips
{
    public class StarShipListViewModel
    {
        public IList<StarShipViewModel> StarShips { get; set; }
        public int Count { get; set; }
    }
}
