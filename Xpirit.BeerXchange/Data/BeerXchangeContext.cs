using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            modelBuilder.Entity<Beer>().HasOne<Beer>(b => b.SwitchedFor);
        }

        public DbSet<Xpirit.BeerXchange.Model.Beer> Beer { get; set; }
    }
}
