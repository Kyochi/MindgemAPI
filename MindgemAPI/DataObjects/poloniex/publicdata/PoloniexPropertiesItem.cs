using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.dataobjects.poloniex.publicdata
{
    public class PoloniexPropertiesItem
    {
        public string URL_PUBLIC_TICKER_POLONIEX { get; }
        public string URL_PUBLIC_DAILYVOLUME_POLONIEX { get; }
        public string URL_PUBLIC_ORDERBOOK_POLONIEX { get; }
        public string URL_PUBLIC_CHARTDATA_POLONIEX { get; }
        public string URL_PUBLIC_CURRENCIES_POLONIEX { get; }
        public string URL_PUBLIC_LOANORDERS_POLONIEX { get; }
        public Double DELAY_REFRESH_TICKER { get; }
        public Double DELAY_REFRESH_PAIRS { get; }

        public PoloniexPropertiesItem()
        {
            URL_PUBLIC_TICKER_POLONIEX = "https://poloniex.com/public?command=returnTicker";
            URL_PUBLIC_CHARTDATA_POLONIEX = "https://poloniex.com/public?command=returnChartData&currencyPair=";
            URL_PUBLIC_DAILYVOLUME_POLONIEX = "https://poloniex.com/public?command=return24hVolume";
            URL_PUBLIC_CURRENCIES_POLONIEX = "https://poloniex.com/public?command=returnCurrencies";
            URL_PUBLIC_ORDERBOOK_POLONIEX = "https://poloniex.com/public?command=returnOrderBook&currencyPair=";
            URL_PUBLIC_LOANORDERS_POLONIEX = "https://poloniex.com/public?command=returnLoanOrders&currency=";
            DELAY_REFRESH_TICKER = 10.0;
            DELAY_REFRESH_PAIRS = 100.0;
        }
    }
}