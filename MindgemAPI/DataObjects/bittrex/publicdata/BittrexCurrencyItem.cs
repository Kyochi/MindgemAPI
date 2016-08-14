using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.bittrex.publicdata
{
    public class BittrexCurrencyItem
    {
        //décomposer https://bittrex.com/api/v1.1/public/getcurrencies
        [JsonProperty("Currency")]
        public String acronym { get; set; }
        [JsonProperty("CurrencyLong")]
        public String name { get; set; }
        [JsonProperty("TxFee")]
        public String txFee { get; set; }
        [JsonProperty("MinConfirmation")]
        public String minConf { get; set; }
        [JsonProperty("BaseAddress")]
        public String depositAddress { get; set; }
        [JsonProperty("CoinType")]
        public String coinType { get; set; }
        [JsonProperty("IsActive")]
        public Boolean isactive { get; set; }
    }
}