using MindgemAPI.converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MindgemAPI.dataobjects
{
    public class TickerItem
    {
        public static readonly String[] defAskInfo = { "price", "wholelotvolume", "lotvolume" };
        public static readonly String[] defBidInfo = { "price", "wholelotvolume", "lotvolume" };
        public static readonly String[] defLastTradeClosed = { "price", "lotvolume" };
        public static readonly String[] defVolume = { "today", "last24hours" };
        public static readonly String[] defPriceWeightAvg = { "today", "last24hours" };
        public static readonly String[] defLowPrice = { "today", "last24hours" };
        public static readonly String[] defHighPrice = { "today", "last24hours" };

        public static readonly String[] defnumberOfTrades = { "today", "last24hours" };

        /*
         *     c = last trade closed array(<price>, <lot volume>),
         *         b = bid array(<price>, <whole lot volume>, <lot volume>),
    v = volume array(<today>, <last 24 hours>),
    p = volume weighted average price array(<today>, <last 24 hours>),
    t = number of trades array(<today>, <last 24 hours>),
    l = low array(<today>, <last 24 hours>),
    h = high array(<today>, <last 24 hours>),
    o = today's opening price
         * */
        [JsonProperty("a")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defAskInfo")]
        public Dictionary<string, object> askInfo;

        [JsonProperty("b")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defBidInfo")]
        public Dictionary<string, object> bidInfo;

        [JsonProperty("c")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defLastTradeClosed")]
        public Dictionary<string, object> lastTradeClosed { get; set; }

        [JsonProperty("v")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defVolume")]
        public Dictionary<string, object> volume { get; set; }

        [JsonProperty("p")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defPriceWeightAvg")]
        public Dictionary<string, object> priceWeightAvg { get; set; }

        [JsonProperty("t")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defnumberOfTrades")]
        public Dictionary<string, object> numberOfTrades;

        [JsonProperty("l")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defLowPrice")]
        public Dictionary<string, object> lowPrice { get; set; }

        [JsonProperty("h")]
        [JsonConverter(typeof(JsonArrayToDictionaryConverter), typeof(TickerItem), "defHighPrice")]
        public Dictionary<string, object> highPrice { get; set; }

        [JsonProperty("o")]
        public string openingPrice { get; set; }
    }
}