using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly BeerXchangeContext context;

        public FridgeService(BeerXchangeContext context)
        {
            this.context = context;
        }

        public async Task AddBeer(Beer beer)
        {
            context.Beer.Add(beer);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Beer> GetAllBeers()
        {
            return context.Beer.ToList();
        }

        public IEnumerable<Beer> GetBeerHistory()
        {
            return context.Beer.Where(b => b.RemovedDate != null || b.RemovedBy != null).OrderByDescending(b => b.RemovedDate).ToList();
        }

        public IEnumerable<Beer> GetCurrentBeers()
        {
            return context.Beer.Where(b => b.RemovedBy == null).ToList();
        }

        public async Task UpdateBeer(Beer beer)
        {
            if (beer is null)
            {
                throw new ArgumentNullException(nameof(beer));
            }

            var existingBeer = context.Beer.Single(b => b.Id == beer.Id);
            if (existingBeer is null)
            {
                throw new ArgumentException(nameof(beer),$"Beer with Id {beer.Id} does not exist");
            }

            context.Update(beer);
            await context.SaveChangesAsync();
        }
    }
}
