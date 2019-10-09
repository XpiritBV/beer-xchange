using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpirit.BeerXchange.Model;
using Xpirit.BeerXchange.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Xpirit.BeerXchange.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BeerController : Controller
    {
        private readonly IBeerService beerService;

        public BeerController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Beer>> Get()
        {
            return await beerService.GetAllBeers();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Beer> Get(int id)
        {
            return await beerService.GetBeerById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Beer beer)
        {
            await beerService.AddBeer(beer);
            return Ok(beer);

        }
    }
}
