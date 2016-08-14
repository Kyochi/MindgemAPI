using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.bittrex.publicdata
{
    public class BittrexPropertiesItem
    {
        public string URL_PUBLIC_TICKER_BITTREX { get; }
        public string URL_PUBLIC_DAILYVOLUME_BITTREX { get; }
        public string URL_PUBLIC_ORDERBOOK_BITTREX { get; }
        public string URL_PUBLIC_CHARTDATA_BITTREX { get; }
        public string URL_PUBLIC_CURRENCIES_BITTREX { get; }
        public string URL_PUBLIC_LOANORDERS_BITTREX { get; }
        public Double DELAY_REFRESH_TICKER { get; }
        public Double DELAY_REFRESH_PAIRS { get; }

        public BittrexPropertiesItem()
        {
            URL_PUBLIC_TICKER_BITTREX = "https://bittrex.com/api/v1.1/public/getticker?market=";
            URL_PUBLIC_CHARTDATA_BITTREX = "";
            URL_PUBLIC_DAILYVOLUME_BITTREX = "";
            URL_PUBLIC_CURRENCIES_BITTREX = "";
            URL_PUBLIC_ORDERBOOK_BITTREX = "";
            URL_PUBLIC_LOANORDERS_BITTREX = "";
            DELAY_REFRESH_TICKER = 10.0;
            DELAY_REFRESH_PAIRS = 100.0;
        }
    }
}