﻿using MindgemAPI.dataobjects;
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
using System.Threading;

namespace MindgemAPI.Models
{
    public class KrakenPublicMarketModel
    {
        public String nameAccount { get; set; }
        public int currentEtherPrice { get; set; }
        private const String URL_PUBLIC_TICKER_KRAKEN = "https://api.kraken.com/0/public/Ticker?pair=";
        private const String URL_PUBLIC_SERVERTIME_KRAKEN = "https://api.kraken.com/0/public/Time";
        private const String URL_PUBLIC_ORDERBOOK_KRAKEN = "https://api.kraken.com/0/public/Depth?pair=";
        private const Double DELAY_REFRESH_TICKER = 10.0;
        private readonly List<String> KRAKEN_PUBLIC_DATA_TYPE = new List<String>(){ "ticker", "server" };

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
        public Double getCurrentKrakenPriceV2(String currencyFrom, String currencyTo)
        {
            try
            {
                String currencyPair = currencyFrom + currencyTo;
                String tickerSearch = "ticker" + currencyPair;
                Object returnedValue;
                DateTime dateActuelle = DateTime.Now;
                if (tickerItemPair.ContainsKey(tickerSearch))
                {
                    
                    if ((dateActuelle - tickerTime[tickerSearch]).TotalSeconds > DELAY_REFRESH_TICKER)
                    {
                        String jsonToDeserialize = getJson("ticker", currencyFrom, currencyTo);
                        KrakenTickerItem ti = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(jsonToDeserialize);
                        tickerTime[tickerSearch] = dateActuelle;
                        tickerItemPair[tickerSearch].askInfo.TryGetValue("price", out returnedValue);

                        Debug.WriteLine("Paire connue et refresh (timeout): " + currencyPair);
                        return Convert.ToDouble(returnedValue, new NumberFormatInfo());
                    }

                    tickerItemPair[tickerSearch].askInfo.TryGetValue("price", out returnedValue);

                    Debug.WriteLine("Paire connue mais pas refresh: " + currencyPair);
                    return Convert.ToDouble(returnedValue, new NumberFormatInfo());
                }

                String jsonToDeserializeNewCurrency = getJson("ticker", currencyFrom, currencyTo);
                KrakenTickerItem newTi = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(jsonToDeserializeNewCurrency);

                tickerItemPair.Add(tickerSearch, newTi);
                tickerTime.Add(tickerSearch, dateActuelle);

                tickerTime[tickerSearch] = dateActuelle;
                tickerItemPair[tickerSearch].askInfo.TryGetValue("price", out returnedValue);

                Debug.WriteLine("Ajout d'une nouvelle paire : " + currencyPair);
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

        // Récupération du cours d'une crypto-monnaie via l'API Kraken
        public Double getCurrentKrakenPrice(String currencyFrom, String currencyTo)
        {
            KrakenTickerItem ti = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(getJson("ticker", currencyFrom, currencyTo));
            if (ti != null)
            {
                Object returnedValue;
                ti.askInfo.TryGetValue("price", out returnedValue);
                return Convert.ToDouble(returnedValue, new NumberFormatInfo());
            }
            return Double.NaN;
        }
        // Récupération du nombre de trades effectués lors des dernières 24 heures 
        public Double getTradesLastDay(String currencyFrom, String currencyTo, String type)
        {
            KrakenTickerItem ti = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(getJson("ticker", currencyFrom,currencyTo));
            if (ti != null)
            {
                Object returnedValue;
                ti.numberOfTrades.TryGetValue(type, out returnedValue);
                return Convert.ToDouble(returnedValue, new NumberFormatInfo());
            }
            return Double.NaN;
        }

        public Double getOpeningPrice(String currencyFrom, String currencyTo)
        {
            KrakenTickerItem ti = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(getJson("ticker", currencyFrom, currencyTo));
            if (ti != null)
            {
                return Convert.ToDouble(ti.openingPrice, new NumberFormatInfo());
            }
            return Double.NaN;
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

        public String getJson(String operationType, String currencyFrom = "", String currencyTo = "")
        {
            try
            {
                String urlKrakenApi = "Unknown url";
                switch (operationType)
                {
                    case "ticker":
                        urlKrakenApi = URL_PUBLIC_TICKER_KRAKEN + currencyFrom + currencyTo;
                        break;
                    case "server":
                        urlKrakenApi = URL_PUBLIC_SERVERTIME_KRAKEN;
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