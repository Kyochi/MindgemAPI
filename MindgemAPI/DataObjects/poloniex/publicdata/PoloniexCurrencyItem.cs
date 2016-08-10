using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.poloniex.publicdata
{
    public class PoloniexCurrencyItem
    {
        // décomposer https://poloniex.com/public?command=returnCurrencies
        // pour créer une instance de currency
        [JsonProperty("id")]
        public String id { get; set; }
        [JsonProperty("name")]
        public String name { get; set; }
        [JsonProperty("txFee")]
        public String txFee { get; set; }
        [JsonProperty("minConf")]
        public String minConf { get; set; }
        [JsonProperty("depositAddress")]
        public String depositAddress { get; set; }
        [JsonProperty("disabled")]
        public String disabled { get; set; }
        [JsonProperty("delisted")]
        public String delisted { get; set; }
        [JsonProperty("frozen")]
        public String frozen { get; set; }
    }
}