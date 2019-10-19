using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xpirit.BeerXchange.Services;

namespace Xpirit.BeerXchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBeerService beerService;

        public UserController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var allBeers = await beerService.GetAllBeers();
            var users = allBeers.Select(b => b.CreatedBy);
            return users.Distinct().OrderBy(u => u).ToList();
        }
    }
}