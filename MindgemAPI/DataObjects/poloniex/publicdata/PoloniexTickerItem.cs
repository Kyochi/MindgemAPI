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
        public static readonly String last;
        [JsonProperty("lowestAsk")]
        public static readonly String lowestAsk;
        [JsonProperty("highestBid")]
        public static readonly String highestBid;
        [JsonProperty("percentChange")]
        public static readonly String percentChange;
        [JsonProperty("baseVolume")]
        public static readonly String baseVolume;
        [JsonProperty("quoteVolume")]
        public static readonly String quoteVolume;

    }
}