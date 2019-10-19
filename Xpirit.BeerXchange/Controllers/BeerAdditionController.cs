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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeerAdditionController : ControllerBase
    {
        private readonly IBeerService beerService;

        public BeerAdditionController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BeerAddition beerAdditionRequest)
        {
            if (beerAdditionRequest == null)
            {
                return BadRequest("Not a valid Beer addition request");
            }

            Beer beer = new Beer();
            beer.Name = beerAdditionRequest.BeerName;
            beer.CreatedBy = beerAdditionRequest.AddedBy;
            beer.Brewery = beerAdditionRequest.Brewery;
            beer.Country = beerAdditionRequest.Country;
            beer.AddedDate = DateTime.Now;

            if (beerAdditionRequest.switchedBeer.HasValue && beerAdditionRequest.switchedBeer.Value != -1)
            {
                var switchedBeer  = await beerService.GetBeerById(beerAdditionRequest.switchedBeer.Value);
                if (switchedBeer.RemovedDate.HasValue || !string.IsNullOrEmpty(switchedBeer.RemovedBy))
                {
                    throw new Exception("invalid switched beer");
                }

                switchedBeer.RemovedDate = DateTime.Now;
                switchedBeer.RemovedBy = beerAdditionRequest.AddedBy;
                await beerService.UpdateBeer(switchedBeer);

                beer.SwitchedFor = switchedBeer;
            }

            await beerService.AddBeer(beer);

            return Ok();
        }
    }
}