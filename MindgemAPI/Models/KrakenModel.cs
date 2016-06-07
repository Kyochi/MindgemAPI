using MindgemAPI.DataObjects;
using MindgemAPI.ScheduledJobs;
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

namespace MindgemAPI.Models
{
    public class KrakenModel
    {
        public String nameAccount { get; set; }
        public int currentEtherPrice { get; set; }
        private const String URL_PUBLIC_TICKER_KRAKEN = "https://api.kraken.com/0/public/Ticker?pair=";
        private const String URL_PUBLIC_SERVERTIME_KRAKEN = "https://api.kraken.com/0/public/Time";
        private const String URL_PUBLIC_ORDERBOOK_KRAKEN = "https://api.kraken.com/0/public/Depth?pair=";

        public KrakenModel()
        {
            // Initialisation du constructeur par défaut
        }

        // Récupération du cours d'une crypto-monnaie via l'API Kraken
        public Double getCurrentKrakenPrice(String currencyFrom, String currencyTo)
        {
            try
            {
                WebResponse response = httpGetRequest(URL_PUBLIC_TICKER_KRAKEN + currencyFrom + currencyTo);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        string currencyValueFromKraken = getPairCode("kraken",currencyFrom, currencyTo);
                        JToken selectSpecificNodeContent = JObject.Parse(json)["result"][currencyValueFromKraken];
                        String jsonfinal = selectSpecificNodeContent.ToString();
                        TickerItem ti = JsonConvert.DeserializeObject<TickerItem>(jsonfinal);
                        //Réfléchir à comment enlever la ligne du dessous
                        //Thèse à écarter : créer un constructeur dans TickerItem
                        ti.mapAskInfo();

                        return Convert.ToDouble(ti.askInfoMapped["price"], new NumberFormatInfo());
                    }
                    else
                    {
                        throw new Exception("Le Json retourné est vide");
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
            return Double.NaN;
        }

        public Double getTradesLastDay(String currencyFrom, String currencyTo)
        {
            try
            {
                WebResponse response = httpGetRequest(URL_PUBLIC_TICKER_KRAKEN + currencyFrom + currencyTo);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        string currencyValueFromKraken = getPairCode("kraken", currencyFrom, currencyTo);
                        JToken selectSpecificNodeContent = JObject.Parse(json)["result"][currencyValueFromKraken];
                        String jsonfinal = selectSpecificNodeContent.ToString();
                        TickerItem ti = JsonConvert.DeserializeObject<TickerItem>(jsonfinal);
                        //Réfléchir à comment enlever la ligne du dessous
                        //Thèse à écarter : créer un constructeur dans TickerItem
                        ti.mapNumberOfTrades();

                        return Convert.ToDouble(ti.numberOfTradesMap["last24hours"], new NumberFormatInfo());
                    }
                    else
                    {
                        throw new Exception("Le Json retourné est vide");
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
            return Double.NaN;
        }

        public Double getOrderBook(String currencyFrom, String currencyTo)
        {
            try
            {
                WebResponse response = httpGetRequest(URL_PUBLIC_ORDERBOOK_KRAKEN + currencyFrom + currencyTo);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        //
                    }
                    else
                    {
                        throw new Exception("Le Json retourné est vide");
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
            return Double.NaN;
        }

        // Récupération du cours de l'Ether via l'API Kraken
        public String getServerTime()
        {
            try
            {
                WebResponse response = httpGetRequest(URL_PUBLIC_SERVERTIME_KRAKEN);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = null;
                    json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        var jsonObject = JsonConvert.DeserializeObject<ServerItem.ServerObject>(json);
                        return Convert.ToString(jsonObject.result.unixtime);
                    }
                    else
                    {
                        throw new Exception("Le Json retourné est vide");
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
            return "";
        }

        private WebResponse httpGetRequest(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";
            return request.GetResponse();
        }

        // Plus tard : à déplacer dans une classe à part qui servira pour toutes les API où on tape.
        public String getPairCode(String service, String from, String to)
        {
            switch(service)
            {
                case "kraken":
                    return "X" + from + "Z" + to;
                default:
                    return "API service not found";
            }
            
        }
    }
}