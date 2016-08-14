using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.bittrex.publicdata
{
    public class BittrexTickerItem
    {
        [JsonProperty("Last")]
        public String last { get; set; }
        [JsonProperty("Ask")]
        public String lowestAsk { get; set; }
        [JsonProperty("Bid")]
        public String highestBid { get; set; }
    }
}