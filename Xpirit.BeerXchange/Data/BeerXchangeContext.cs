using Microsoft.EntityFrameworkCore;
using Xpirit.BeerXchange.Model;

namespace Xpirit.BeerXchange
{
	public class BeerXchangeContext : DbContext
    {
        public BeerXchangeContext (DbContextOptions<BeerXchangeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>().HasOne(b => b.SwitchedFor);
        }

        public DbSet<Beer> Beer { get; set; }
    }
}
