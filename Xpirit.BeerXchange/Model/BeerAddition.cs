using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Xpirit.BeerXchange.Model
{
    public class BeerAddition
    {
        [JsonProperty("beerName")]
        public string BeerName { get; set; }

        [JsonProperty("brewery")]
        public string Brewery { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("addedBy")]
        public string AddedBy { get; set; }

        [JsonProperty("switchedBeer")]
        public int? switchedBeer { get; set; }
    }
}
