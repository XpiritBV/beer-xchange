using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xpirit.BeerXchange.Model
{
    public class BeerRemoval
    {
        [JsonProperty("beerId")]
        public int BeerId { get; set; }
    }
}
