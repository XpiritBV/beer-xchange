using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xpirit.BeerXchange.Model
{
    public class BeerDrinker
    {
        public string Name { get; set; }
        public string Credits { get; set; }
        public IEnumerable<Beer> AddedBeers { get; set; }
        public IEnumerable<Beer> DrankBeers { get; set; }
    }
}
