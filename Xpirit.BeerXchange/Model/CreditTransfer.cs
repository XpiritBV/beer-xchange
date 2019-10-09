using Newtonsoft.Json;

namespace Xpirit.BeerXchange.Model
{
    public class CreditTransfer
    {
        [JsonProperty("beerId")]
        public int BeerId { get; set; }

        [JsonProperty("creditReceiver")]
        public string CreditReceiver { get; set; }
    }
}
