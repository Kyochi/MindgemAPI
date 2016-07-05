using MindgemAPI.converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.kraken.publicdata
{
    public class KrakenPairItem
    {
        [JsonProperty("result")]
        [JsonConverter(typeof(JsonObjectsToListConverter))]
        public List<String> krakenPairs { get; set; }
    }
}