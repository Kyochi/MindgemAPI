using MindgemAPI.dataobjects;
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
        private readonly List<String> KRAKEN_PUBLIC_DATA_TYPE = new List<String>(){ "ticker", "server" };

        public UrlBuilder urlBuilder;
        public DataObjectProvider dataObjectProvider;
        public static Dictionary<String, System.Threading.Timer> loader = new Dictionary<string, System.Threading.Timer>();

        public static Dictionary<String, KrakenTickerItem> tickerItemPair = new Dictionary<string, KrakenTickerItem>();

        public static List<String> listest = new List<string>();

        public KrakenPublicMarketModel()
        {
            urlBuilder = new UrlBuilder();
            dataObjectProvider = new DataObjectProvider();
        }
        

        public void reLoadDataModel(Object etat)
        {
            // On récupère les paramètres avec le handle
            CustomParametersWithWaitHandle parametres = (CustomParametersWithWaitHandle)etat;
            string dataModelToUpdate = parametres.dataModel;
            string currencyFrom = parametres.currFrom;
            string currencyTo = parametres.currTo;

            AutoResetEvent wh = parametres.WaitHandle;
            
            String jsonToDeserialize = getJson(dataModelToUpdate, currencyFrom, currencyTo);
            KrakenTickerItem ti = dataObjectProvider.deserializeJsonToObject<KrakenTickerItem>(jsonToDeserialize);
            String currency = currencyFrom + currencyTo;
            if (tickerItemPair.ContainsKey(currency))
            {
                tickerItemPair[currency] = ti;
                Debug.WriteLine("Prix refresh pair : " + currency  + " = " + tickerItemPair[currency].askInfo["price"]);
            }
            else
            {
                tickerItemPair.Add(currency, ti);
                Debug.WriteLine("Nouveau prix pair : " + currency + " = " + tickerItemPair[currency].askInfo["price"]);
            }

            wh.Set();
        }
       
        // Récupération du cours d'une crypto-monnaie via l'API Kraken
        public Double getCurrentKrakenPriceV2(String currencyFrom, String currencyTo)
        {

            try
            {
                String currencyPair = currencyFrom + currencyTo;
                if (loader.ContainsKey("ticker" + currencyPair))
                {
                    Object returnedValue;
                    tickerItemPair[currencyPair].askInfo.TryGetValue("price", out returnedValue);
                    System.Diagnostics.Debug.WriteLine("Renvoi du prix au client sans appel kraken : " + returnedValue);
                    return Convert.ToDouble(returnedValue, new NumberFormatInfo());
                }
                else
                {
                    AutoResetEvent wh = new AutoResetEvent(false);
                    var mesParametres = new CustomParametersWithWaitHandle(wh, "ticker", currencyFrom, currencyTo);

                    loader.Add("ticker" + currencyPair, new Timer(reLoadDataModel, mesParametres, 0, 10000));
                    //Thread.Sleep(3000);
                    wh.WaitOne();
                    
                    if (tickerItemPair.ContainsKey(currencyPair))
                    {
                        Object returnedValue;
                        tickerItemPair[currencyPair].askInfo.TryGetValue("price", out returnedValue);
                        return Convert.ToDouble(returnedValue, new NumberFormatInfo());
                    }
                    return Double.NaN;
                }
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