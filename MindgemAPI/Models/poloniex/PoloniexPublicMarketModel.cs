using MindgemAPI.dataobjects.poloniex.publicdata;
using MindgemAPI.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public PoloniexPropertiesItem ppi = new PoloniexPropertiesItem();
        public PoloniexPairItem ppairs = new PoloniexPairItem();
        public static Dictionary<String, System.Threading.Timer> loader = new Dictionary<string, System.Threading.Timer>();

        public static Dictionary<String, PoloniexTickerItem> tickerItemPair = new Dictionary<string, PoloniexTickerItem>();
        public static Dictionary<String, DateTime> tickerTime = new Dictionary<string, DateTime>();

        public UrlBuilder urlBuilder;
        public DataObjectProvider dataObjectProvider;

        public PoloniexPublicMarketModel()
        {
            urlBuilder = new UrlBuilder();
            dataObjectProvider = new DataObjectProvider();
        }

        public Double getCurrentTickerInfos(String operationType, String currencyFrom, String currencyTo)
        {
            try
            {
                String currencyPair = currencyFrom + currencyTo;
                String tickerSearch = "ticker" + currencyPair;
                Object returnedValue;
                DateTime dateActuelle = DateTime.Now;

                /*if (kpairs.krakenPairs.Contains(urlBuilder.getPairCode("kraken", currencyFrom, currencyTo)))
                {*/
                    //checkpoint
                    if (tickerItemPair.ContainsKey(tickerSearch))
                    {

                        if ((dateActuelle - tickerTime[tickerSearch]).TotalSeconds > ppi.DELAY_REFRESH_TICKER)
                        {
                            String jsonToDeserialize = getJson("ticker", currencyFrom, currencyTo);
                            PoloniexTickerItem ti = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(jsonToDeserialize);
                            tickerTime[tickerSearch] = dateActuelle;

                            Debug.WriteLine("Paire connue et refresh (timeout): " + currencyPair);
                        }

                        else
                        {
                            Debug.WriteLine("Paire connue mais pas refresh: " + currencyPair);
                        }
                    }
                    else
                    {
                        String jsonToDeserializeNewCurrency = getJson("ticker", currencyFrom, currencyTo);
                        PoloniexTickerItem newTi = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(jsonToDeserializeNewCurrency);

                        tickerItemPair.Add(tickerSearch, newTi);
                        tickerTime.Add(tickerSearch, dateActuelle);

                        tickerTime[tickerSearch] = dateActuelle;

                        Debug.WriteLine("Ajout d'une nouvelle paire : " + currencyPair);
                    }


                    switch (operationType)
                    {
                        case "last":
                            returnedValue = tickerItemPair[tickerSearch].last;
                            break;
                        case "lowestAsk":
                            returnedValue = tickerItemPair[tickerSearch].lowestAsk;
                            break;
                        case "highestBid":
                            returnedValue = tickerItemPair[tickerSearch].highestBid;
                            break;
                        case "percentChange":
                            returnedValue = tickerItemPair[tickerSearch].percentChange;
                            break;
                        case "baseVolume":
                            returnedValue = tickerItemPair[tickerSearch].baseVolume;
                            break;
                        case "quoteVolume":
                            returnedValue = tickerItemPair[tickerSearch].quoteVolume;
                            break;
                        default:
                            Debug.WriteLine("Opération inconnue");
                            returnedValue = Double.NaN;
                            break;
                    }
                /*}
                else
                {
                    Debug.WriteLine("Paire inconnue");
                    returnedValue = Double.NaN;
                }*/


                return Convert.ToDouble(returnedValue, new NumberFormatInfo());

            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine("Problème dans la déserialisation du json : " + jsonEx.Message);
                return Double.NaN;
            }
            catch (ArgumentNullException argNullEx)
            {
                Debug.WriteLine("La valeur demandée est nulle : " + argNullEx.Message);
                return Double.NaN;
            }
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
            PoloniexTickerItem pti = null;
            if (currencyFrom == "EUR") return Double.NaN;

            if (currencyFrom == null || currencyTo == null)
            {
                return Double.NaN;
            }
            else if (!currencyFrom.Equals("BTC"))
            {
                pti = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", "BTC", currencyFrom));
            }
            else
            {
                pti = dataObjectProvider.deserializeJsonToObject<PoloniexTickerItem>(getJson("ticker", "BTC", currencyTo));

            }

            if (pti != null && currencyTo == "EUR")
            {
                Double rateBitcoin = Convert.ToDouble(pti.last, new NumberFormatInfo());
                // Récupérer le cours réel du bitcoin
                KrakenPublicMarketModel kpmm = new KrakenPublicMarketModel();
                Double bitcoinPrice = kpmm.getCurrentTickerInfos("askInfo", "price", "XBT",currencyTo);
                Console.WriteLine(rateBitcoin * bitcoinPrice);
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

        public String getCurrencyDetails(String currencyFrom, String operationType)
        {
            PoloniexCurrencyItem pci = dataObjectProvider.deserializeJsonToObject<PoloniexCurrencyItem>(getJson("currency", currencyFrom));
            if (pci != null)
            {
                switch (operationType)
                {
                    case "id":
                        return pci.id;
                    case "name":
                        return pci.name;
                    case "txFee":
                        return pci.txFee;
                    case "minConf":
                        return pci.minConf;
                    case "depositAddress":
                        return pci.depositAddress;
                    case "disabled":
                        return pci.disabled;
                    case "delisted":
                        return pci.delisted;
                    case "frozen":
                        return pci.frozen;
                    default:
                        break;
                }
            }
            return String.Empty;
        }

        public String getJson(String operationType, String currencyFrom = "", String currencyTo = "")
        {
            try
            {
                String urlPoloniexApi = "Unknown url";
                switch (operationType)
                {
                    case "ticker":
                        urlPoloniexApi = ppi.URL_PUBLIC_TICKER_POLONIEX;
                        break;
                    case "currency":
                        urlPoloniexApi = ppi.URL_PUBLIC_CURRENCIES_POLONIEX;
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
                            case "currency":
                                selectSpecificNodeContent = JObject.Parse(json)[currencyFrom];
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