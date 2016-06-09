using MindgemAPI.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.DataObjects
{
    public class TickerItem
    {
        //public static readonly List<string[]> defAskInfo = new List<string[]>(9) {}
        //{ "price", "avgprice", "maxprice" };
        public static readonly String[] defAskInfo = { "price", "avgprice", "maxprice" };

        [JsonProperty("a")]
        [JsonConverter(typeof(CustomJsonConverter), typeof(TickerItem), "defAskInfo")]
        public Dictionary<string, object> priceInfo;

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

        public Dictionary<String, String> askInfoMapped { get; set; }
        public Dictionary<String, String> numberOfTradesMap { get; set; }

        public void mapAskInfo()
        {
            Dictionary<String, String> resultMap = new Dictionary<string, string>();
            resultMap.Add("price", askInfo.ElementAt(0));
            resultMap.Add("wholelotvolume", askInfo.ElementAt(1));
            resultMap.Add("lotvolume", askInfo.ElementAt(2));
            askInfoMapped = resultMap;
        }

        public void mapNumberOfTrades()
        {
            Dictionary<String, String> resultMap = new Dictionary<string, string>();
            resultMap.Add("today", Convert.ToString(numberOfTrades.ElementAt(0)));
            resultMap.Add("last24hours", Convert.ToString(numberOfTrades.ElementAt(1)));
            numberOfTradesMap = resultMap;
        }
    }
}