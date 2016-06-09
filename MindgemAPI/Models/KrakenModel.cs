﻿using MindgemAPI.dataobjects;
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
            TickerItem ti = JsonConvert.DeserializeObject<TickerItem>(getJson("ticker", currencyFrom, currencyTo));
            if (ti != null)
            {
                Object returnedValue;
                ti.askInfo.TryGetValue("price", out returnedValue);
                return Convert.ToDouble(returnedValue, new NumberFormatInfo());
            }
            return Double.NaN;

        }

        // Récupération du nombre de trades effectués lors des dernières 24 heures 
        public Double getTradesLastDay(String currencyFrom, String currencyTo)
        {                        
            TickerItem ti = JsonConvert.DeserializeObject<TickerItem>(getJson("ticker", currencyFrom,currencyTo));
            if (ti != null)
            {
                Object returnedValue;
                ti.numberOfTrades.TryGetValue("last24hours", out returnedValue);
                return Convert.ToDouble(returnedValue, new NumberFormatInfo());
            }
            return Double.NaN;
        }

        // Récupération de l'heure du serveur Kraken
        public String getServerTime()
        {
            //TODO : reprendre quand le commit des DataObjects sera à jour
            //ServerItem si = JsonConvert.DeserializeObject<ServerItem.ServerObject>(getJson("server"));
            //return Convert.ToString(si.unixtime);
            return "Indisponible";
        }

        /*-----------*/
        /*-- UTILS --*/
        /*-----------*/
        private WebResponse httpGetRequest(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";
            return request.GetResponse();
        }

        public String getJson(String operationType, String currencyFrom = "", String currencyTo = "")
        {
            try
            {
                String urlKrakenApi = "";
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
                WebResponse response = httpGetRequest(urlKrakenApi);
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
                                string pair = getPairCode("kraken", currencyFrom, currencyTo);
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

            return String.Empty;
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