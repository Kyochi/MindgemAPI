using MindgemAPI.dataobjects.poloniex.publicdata;
using MindgemAPI.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        public Double getPercentChange(String currencyFrom, String currencyTo)
        {
            PoloniexTickerItem pti = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", currencyFrom, currencyTo));
            if (pti != null)
            {
                return Convert.ToDouble(pti.percentChange, new NumberFormatInfo());
            }
            return Double.NaN;
        }


        public Double getPriceCurrency(String currencyFrom, String currencyTo)
        {
            PoloniexTickerItem pti = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", "BTC", currencyFrom));
            if (pti != null)
            {
                Double rateBitcoin = Convert.ToDouble(pti.last, new NumberFormatInfo());
                // Récupérer le cours réel du bitcoin
                KrakenPublicMarketModel kpmm = new KrakenPublicMarketModel();
                Double bitcoinPrice = kpmm.getCurrentKrakenPrice("XBT",currencyTo);

                return rateBitcoin * bitcoinPrice;
            }
            return Double.NaN;
        }

        public Double getLastExchangeRate(String currencyFrom, String currencyTo)
        {
            PoloniexTickerItem ptiBitcoin = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", "BTC", currencyFrom));
            PoloniexTickerItem ptiTarget = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", "BTC", currencyTo));
            if (ptiBitcoin != null && ptiTarget != null)
            {
                Double rateBitcoin = Convert.ToDouble(ptiBitcoin.last, new NumberFormatInfo());
                Double rateTarget = Convert.ToDouble(ptiBitcoin.last, new NumberFormatInfo());

                return rateBitcoin / rateTarget;
            }
            return Double.NaN;
        }

        //Par défaut : Bitcoin
        public Double getLastExchangeRate(String currencyFrom)
        {
            PoloniexTickerItem pti = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", "BTC", currencyFrom));
            if (pti != null)
            {
                return Convert.ToDouble(pti.last, new NumberFormatInfo());
            }
            return Double.NaN;
        }

        public String getJson(String operationType, String currencyFrom = "", String currencyTo = "")
        {
            try
            {
                String urlPoloniexApi = "Unknown url";
                switch (operationType)
                {
                    case "ticker":
                        urlPoloniexApi = URL_PUBLIC_TICKER_POLONIEX;
                        break;
                    default:
                        break;
                }
                WebResponse response = utils.HttpRequest.getRequest(urlPoloniexApi);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        JToken selectSpecificNodeContent = null;
                        switch (operationType)
                        {
                            case "ticker":
                                string pair = urlBuilder.getPairCode("poloniex", currencyFrom, currencyTo);
                                selectSpecificNodeContent = JObject.Parse(json)[pair];
                                return selectSpecificNodeContent.ToString();
                            default:
                                break;
                        }

                    }
                    else
                    {
                        throw new JsonException("Le Json retourné est vide ");
                    }
                }


            }
            catch (WebException webEx)
            {
                Console.WriteLine("Le délai d'attente a été dépassé ou une erreur s'est produite pendant le traitement de la requête");
                Console.WriteLine("Message d'exception : " + webEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception inconnue : " + e.Message);
            }

            return String.Empty;
        }
    }
}