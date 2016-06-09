using MindgemAPI.converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindgemAPI.dataobjects
{
    public class TickerItem
    {
        public static readonly String[] defAskInfo = { "price", "wholelotvolume", "lotvolume" };
        public static readonly String[] defnumberOfTrades = { "today", "last24hours" };

        [JsonProperty("a")]
        [JsonConverter(typeof(JsonTickerConverter), typeof(TickerItem), "defAskInfo")]
        public Dictionary<string, object> askInfo;
        [JsonProperty("b")]
        public List<string> bidInfo { get; set; }
        [JsonProperty("c")]
        public List<string> lastTradeClosed { get; set; }
        [JsonProperty("v")]
        public List<string> volume { get; set; }
        [JsonProperty("p")]
        public List<string> priceWeightAvg { get; set; }
        [JsonProperty("t")]
        [JsonConverter(typeof(JsonTickerConverter), typeof(TickerItem), "defnumberOfTrades")]
        public Dictionary<string, object> numberOfTrades;
        [JsonProperty("l")]
        public List<string> lowPrice { get; set; }
        [JsonProperty("h")]
        public List<string> highPrice { get; set; }
        [JsonProperty("o")]
        public string openingPrice { get; set; }
    }
}