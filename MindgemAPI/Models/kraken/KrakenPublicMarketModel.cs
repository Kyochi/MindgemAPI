using MindgemAPI.dataobjects;
using MindgemAPI.dataobjects.kraken.publicdata;
using MindgemAPI.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace MindgemAPI.Models
{
    public class KrakenPublicMarketModel
    {

        public KrakenPropertiesItem kpi = new KrakenPropertiesItem();
        public KrakenPairItem kpairs = new KrakenPairItem();
        public UrlBuilder urlBuilder;
        public DataObjectProvider dataObjectProvider;
        public static Dictionary<String, System.Threading.Timer> loader = new Dictionary<string, System.Threading.Timer>();

        public static Dictionary<String, KrakenTickerItem> tickerItemPair = new Dictionary<string, KrakenTickerItem>();
        public static Dictionary<String, DateTime> tickerTime = new Dictionary<string, DateTime>();
        
        public KrakenPublicMarketModel()
        {
            urlBuilder = new UrlBuilder();
            dataObjectProvider = new DataObjectProvider();
        }

        // Récupération du cours d'une crypto-monnaie via l'API Kraken
        public Double getCurrentTickerInfos(String operationType, String operationCode, String currencyFrom, String currencyTo)
        {
            try
            {
                String currencyPair = currencyFrom + currencyTo;
                String tickerSearch = "ticker" + currencyPair;
                Object returnedValue;
                DateTime dateActuelle = DateTime.Now;

                String pairToDeserialize = getJson("assetpairs");
                kpairs = dataObjectProvider.deserializeJsonToObject<KrakenPairItem>(pairToDeserialize);

                if (kpairs.krakenPairs.Contains(urlBuilder.getPairCode("kraken", currencyFrom, currencyTo)))
                {
                    //checkpoint
                    if (tickerItemPair.ContainsKey(tickerSearch))
                    {

                        if ((dateActuelle - tickerTime[tickerSearch]).TotalSeconds > kpi.DELAY_REFRESH_TICKER)
                        {
                            String jsonToDeserialize = getJson("ticker", currencyFrom, currencyTo);
                            KrakenTickerItem ti = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(jsonToDeserialize);
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
                        KrakenTickerItem newTi = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(jsonToDeserializeNewCurrency);

                        tickerItemPair.Add(tickerSearch, newTi);
                        tickerTime.Add(tickerSearch, dateActuelle);

                        tickerTime[tickerSearch] = dateActuelle;

                        Debug.WriteLine("Ajout d'une nouvelle paire : " + currencyPair);
                    }
                

                    switch (operationType)
                    {
                        case "askInfo":
                            tickerItemPair[tickerSearch].askInfo.TryGetValue(operationCode, out returnedValue);
                            break;
                        case "bidInfo":
                            tickerItemPair[tickerSearch].bidInfo.TryGetValue(operationCode, out returnedValue);
                            break;
                        case "volume":
                            tickerItemPair[tickerSearch].volume.TryGetValue(operationCode, out returnedValue);
                            break;
                        case "numberOfTrades":
                            tickerItemPair[tickerSearch].numberOfTrades.TryGetValue(operationCode, out returnedValue);
                            break;
                        case "lowPrice":
                            tickerItemPair[tickerSearch].lowPrice.TryGetValue(operationCode, out returnedValue);
                            break;
                        case "highPrice":
                            tickerItemPair[tickerSearch].highPrice.TryGetValue(operationCode, out returnedValue);
                            break;
                        case "opening":
                            returnedValue = tickerItemPair[tickerSearch].openingPrice;
                            break;
                        default:
                            Debug.WriteLine("Opération inconnue");
                            returnedValue = Double.NaN;
                            break;
                    }
                } else
                {
                    Debug.WriteLine("Paire inconnue");
                    returnedValue = Double.NaN;
                }


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

        // Récupération de l'heure du serveur Kraken
        public String getServerTime(String type)
        {
            KrakenServerItem si = dataObjectProvider.deserializeJsonToObject<KrakenServerItem>(getJson("server"));
            if (si != null)
            {
                if (type.Equals("unixtime"))
                {
                    return Convert.ToString(si.unixtime);
                }
                else if (type.Equals("rfc"))
                {
                    return si.rfc;
                }
                else
                {
                    return String.Empty;
                }
                
            }
            return String.Empty;
        }

        public String getKrakenPairs()
        {
            KrakenPairItem kpairtest = dataObjectProvider.deserializeJsonToObject<KrakenPairItem>(getJson("assetpairs"));
            if (kpairtest != null)
            {
                return Convert.ToString(kpairtest.krakenPairs);

            }
            return String.Empty;
        }

        public String getJson(String operationType, String currencyFrom = "", String currencyTo = "")
        {
            try
            {
                String urlKrakenApi = "Unknown url";
                switch (operationType)
                {
                    case "ticker":
                        urlKrakenApi = kpi.URL_PUBLIC_TICKER_KRAKEN + currencyFrom + currencyTo;
                        break;
                    case "server":
                        urlKrakenApi = kpi.URL_PUBLIC_SERVERTIME_KRAKEN;
                        break;
                    case "assetpairs":
                        urlKrakenApi = kpi.URL_PUBLIC_ASSETPAIRS_KRAKEN;
                        break;
                    default:
                        break;
                }
                WebResponse response = HttpRequest.getRequest(urlKrakenApi);
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
                                string pair = urlBuilder.getPairCode("kraken", currencyFrom, currencyTo);
                                selectSpecificNodeContent = JObject.Parse(json)["result"][pair];
                                return selectSpecificNodeContent.ToString();
                            case "server":
                                selectSpecificNodeContent = JObject.Parse(json)["result"];
                                return selectSpecificNodeContent.ToString();
                            case "assetpairs":
                                selectSpecificNodeContent = JObject.Parse(json);
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