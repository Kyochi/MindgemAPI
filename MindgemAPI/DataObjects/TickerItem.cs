using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.DataObjects
{
    public class TickerItem
    {
        [JsonProperty("a")]
        public List<string> askInfo { get; set; }
        [JsonProperty("b")]
        public List<string> bidInfo { get; set; }
        [JsonProperty("c")]
        public List<string> lastTradeClosed { get; set; }
        [JsonProperty("v")]
        public List<string> volume { get; set; }
        [JsonProperty("p")]
        public List<string> priceWeightAvg { get; set; }
        [JsonProperty("t")]
        public List<int> numberOfTrades { get; set; }
        [JsonProperty("l")]
        public List<string> lowPrice { get; set; }
        [JsonProperty("h")]
        public List<string> highPrice { get; set; }
        [JsonProperty("o")]
        public string openingPrice { get; set; }
    }
}