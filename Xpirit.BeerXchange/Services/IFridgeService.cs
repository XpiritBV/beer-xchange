using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange.Services
{
    interface IFridgeService
    {
        IEnumerable<Beer> GetAllBeers();
        IEnumerable<Beer> GetCurrentBeers();
        IEnumerable<Beer> GetBeerHistory();

        bool AddBeer(Beer beer);
        bool UpdateBeer(Beer beer);


        //Future additions?

        //IEnumerable<BeerDrinker> GetAllBeerDrinkers();
        //int GetUserCredits();
    }
}
