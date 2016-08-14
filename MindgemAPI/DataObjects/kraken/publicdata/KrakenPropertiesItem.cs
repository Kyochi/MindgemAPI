using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MindgemAPI.dataobjects.kraken.publicdata
{
    public class KrakenPropertiesItem
    {
        public string URL_PUBLIC_TICKER_KRAKEN { get; }
        public string URL_PUBLIC_SERVERTIME_KRAKEN { get; }
        public string URL_PUBLIC_ORDERBOOK_KRAKEN { get; }
        public string URL_PUBLIC_ASSETPAIRS_KRAKEN { get; }
        public String URL_PRIVATE_KRAKEN { get; }
        public Double DELAY_REFRESH_TICKER { get; }
        public Double DELAY_REFRESH_PAIRS { get; }

        public const int KRAKEN_API_VERSION = 0;

        public KrakenPropertiesItem ()
        {
            URL_PUBLIC_TICKER_KRAKEN = @"https://api.kraken.com/0/public/Ticker?pair=";
            URL_PUBLIC_ASSETPAIRS_KRAKEN = @"https://api.kraken.com/0/public/AssetPairs";
            URL_PUBLIC_SERVERTIME_KRAKEN = @"https://api.kraken.com/0/public/Time";
            URL_PUBLIC_ORDERBOOK_KRAKEN = @"https://api.kraken.com/0/public/Depth?pair=";
            URL_PRIVATE_KRAKEN = String.Concat(@"https://api.kraken.com/", KRAKEN_API_VERSION, "/private/");
            DELAY_REFRESH_TICKER = 10.0;
            DELAY_REFRESH_PAIRS = 100.0;
        }
    }
}