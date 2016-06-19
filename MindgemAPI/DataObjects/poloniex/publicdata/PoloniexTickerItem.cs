using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.poloniex.publicdata
{
    public class PoloniexTickerItem
    {
        [JsonProperty("last")]
        public String last { get; set; }
        [JsonProperty("lowestAsk")]
        public String lowestAsk { get; set; }
        [JsonProperty("highestBid")]
        public String highestBid { get; set; }
        [JsonProperty("percentChange")]
        public String percentChange { get; set; }
        [JsonProperty("baseVolume")]
        public String baseVolume { get; set; }
        [JsonProperty("quoteVolume")]
        public String quoteVolume { get; set; }

    }
}