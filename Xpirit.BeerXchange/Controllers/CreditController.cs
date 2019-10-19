using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xpirit.BeerXchange.Model;
using Xpirit.BeerXchange.Services;

namespace Xpirit.BeerXchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : Controller
    {
        private readonly IBeerService beerService;

        public CreditController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<int> Get(string id)
        {
            return await beerService.GetUserCredits(id);
        }
    }
}
