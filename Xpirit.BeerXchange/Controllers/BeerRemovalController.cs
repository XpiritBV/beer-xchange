using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xpirit.BeerXchange.Model;
using Xpirit.BeerXchange.Services;

namespace Xpirit.BeerXchange.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeerRemovalController : ControllerBase
    {
        private readonly IBeerService beerService;

        public BeerRemovalController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BeerRemoval beerRemovalRequest)
        {
            if (beerRemovalRequest == null)
            {
                return BadRequest("Not a valid Beer removal request");
            }

            var beer = await beerService.GetBeerById(beerRemovalRequest.BeerId);
            beer.RemovedDate = DateTime.Now;
            beer.RemovedBy = User.Identity.Name;
            var currentBeers = await beerService.GetCurrentBeers();
            beer.SwitchedFor = currentBeers.First(b => b.CreatedBy == User.Identity.Name);

            await beerService.UpdateBeer(beer);

            return Ok();
        }
    }
}