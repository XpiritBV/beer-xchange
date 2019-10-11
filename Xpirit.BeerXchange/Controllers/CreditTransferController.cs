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
    public class CreditTransferController : ControllerBase
    {
        private readonly IBeerService beerService;

        public CreditTransferController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreditTransfer creditTransfer)
        {
            var beer = await beerService.GetBeerById(creditTransfer.BeerId);
            if (beer == null)
            {
                return BadRequest("invalid beerId");
            }

            beer.CreatedBy = creditTransfer.CreditReceiver;

            await beerService.UpdateBeer(beer);

            return Ok();
        }
    }
}