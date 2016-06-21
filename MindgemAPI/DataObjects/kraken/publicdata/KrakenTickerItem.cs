using MindgemAPI.converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindgemAPI.dataobjects
{
    public class KrakenTickerItem
    {
        public static readonly String[] defAskInfo = { "price", "wholelotvolume", "lotvolume" };
        public static readonly String[] defBidInfo = { "price", "wholelotvolume", "lotvolume" };
        public static readonly String[] defLastTradeClosed = { "price", "lotvolume" };
        public static readonly String[] defVolume = { "today", "last24hours" };
        public static readonly String[] defPriceWeightAvg = { "today", "last24hours" };
        public static readonly String[] defLowPrice = { "today", "last24hours" };
        public static readonly String[] defHighPrice = { "today", "last24hours" };

        public static readonly String[] defnumberOfTrades = { "today", "last24hours" };
        
        [JsonProperty("a")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defAskInfo")]
        public Dictionary<string, object> askInfo;

        [JsonProperty("b")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defBidInfo")]
        public Dictionary<string, object> bidInfo;

        [JsonProperty("c")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defLastTradeClosed")]
        public Dictionary<string, object> lastTradeClosed { get; set; }

        [JsonProperty("v")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defVolume")]
        public Dictionary<string, object> volume { get; set; }

        [JsonProperty("p")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defPriceWeightAvg")]
        public Dictionary<string, object> priceWeightAvg { get; set; }

        [JsonProperty("t")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defnumberOfTrades")]
        public Dictionary<string, object> numberOfTrades;

        [JsonProperty("l")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defLowPrice")]
        public Dictionary<string, object> lowPrice { get; set; }

        [JsonProperty("h")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(KrakenTickerItem), "defHighPrice")]
        public Dictionary<string, object> highPrice { get; set; }

        [JsonProperty("o")]
        public string openingPrice { get; set; }
    }
}