using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange.Services
{
    public interface IBeerService
    {
        Task<IEnumerable<Beer>> GetAllBeers();
        Task<IEnumerable<Beer>> GetCurrentBeers();
        Task<IEnumerable<Beer>> GetBeerHistory();
        Task<Beer> GetBeerById(int id);

        Task AddBeer(Beer beer);
        Task UpdateBeer(Beer beer);


        //Future additions?

        //IEnumerable<BeerDrinker> GetAllBeerDrinkers();
        Task<int> GetUserCredits(string user);
    }
}
