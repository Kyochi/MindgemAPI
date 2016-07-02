using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.kraken.publicdata
{
    public class KrakenPropertiesItem
    {
        public string URL_PUBLIC_TICKER_KRAKEN { get; }
        public string URL_PUBLIC_SERVERTIME_KRAKEN { get; }
        public string URL_PUBLIC_ORDERBOOK_KRAKEN { get; }
        public string URL_PUBLIC_ASSETPAIRS_KRAKEN { get; }
        public Double DELAY_REFRESH_TICKER { get; }

        public KrakenPropertiesItem ()
        {
            URL_PUBLIC_TICKER_KRAKEN = "https://api.kraken.com/0/public/Ticker?pair=";
            URL_PUBLIC_ASSETPAIRS_KRAKEN = "https://api.kraken.com/0/public/AssetPairs";
            URL_PUBLIC_SERVERTIME_KRAKEN = "https://api.kraken.com/0/public/Time";
            URL_PUBLIC_ORDERBOOK_KRAKEN = "https://api.kraken.com/0/public/Depth?pair=";
            DELAY_REFRESH_TICKER = 10.0;
        }
    }
}