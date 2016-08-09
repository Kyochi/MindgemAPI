using MindgemAPI.converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.poloniex.publicdata
{
    public class PoloniexPairItem
    {
        [JsonProperty("result")]
        [JsonConverter(typeof(JsonObjectsToListConverter))]
        public List<String> poloniexCurrencies { get; set; }
        public List<String> poloniexPairs { get; set; }
    }
}