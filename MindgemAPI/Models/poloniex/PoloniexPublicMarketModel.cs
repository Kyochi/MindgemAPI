using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.Models.poloniex
{
    public class PoloniexPublicMarketModel
    {
        private const String URL_PUBLIC_TICKER_POLONIEX = "https://poloniex.com/public?command=returnTicker";
        private const String URL_PUBLIC_SERVERTIME_POLONIEX = "";
        private const String URL_PUBLIC_ORDERBOOK_POLONIEX = "";

        public UrlBuilder urlBuilder;
        public DataObjectProvider dataObjectProvider;

        public PoloniexPublicMarketModel()
        {
            urlBuilder = new UrlBuilder();
            dataObjectProvider = new DataObjectProvider();
        }
    }
}