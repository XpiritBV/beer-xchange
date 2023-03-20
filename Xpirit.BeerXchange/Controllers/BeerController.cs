using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.KernelExtensions;
using Xpirit.BeerXchange.Model;
using Xpirit.BeerXchange.Services;

namespace Xpirit.BeerXchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : Controller
    {
        private readonly IBeerService beerService;
        private readonly IKernel _kernel;

        public BeerController(IBeerService beerService, IKernel kernel)
        {
            _kernel = kernel;
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

        // GET Method explain my beer by id
        [HttpGet("{id}/explain")]
        public async Task<ExplainationResult> Explain(int id)
        {
            var beer = await beerService.GetBeerById(id);

            var systemPrompt =
                """
I want you to act as a Cicerone
I will give you the details of my beer and you will give me a explaination of that beer
I want you to be convincing so that my friends will buy this beer
I also want you to make it one paragraph
""";

            var beerDetails = $"Name: {beer.Name}, Brewery: {beer.Brewery}";

            var beerDetailsFunction = _kernel.CreateSemanticFunction(systemPrompt);

            var beerExplanation = await _kernel.RunAsync(beerDetails, beerDetailsFunction);

            return new ExplainationResult
            {
                Explaination = beerExplanation.Result
            };
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Beer beer)
        {
            await beerService.AddBeer(beer);
            return Ok(beer);

        }
    }
}
