using MindgemAPI.DataObjects;
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
        private const String URL_PUBLIC_ASSET_KRAKEN = "https://api.kraken.com/0/public/Ticker?pair=";

        public KrakenModel()
        {
            // Initialisation du constructeur par défaut
        }

        // Récupération du cours d'une crypto-monnaie via l'API Kraken
        public Double getCurrentKrakenPrice(String currencyFrom, String currencyTo)
        {
            try
            {
                WebResponse response = httpGetRequest(URL_PUBLIC_ASSET_KRAKEN + currencyFrom + currencyTo);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = null;
                    json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        // A voir si on peut rendre plus générique sans mettre X et Z en dur et sans trop augmenter la complexité du code.
                        // Dans un second temps : rendre plus propre l'appel au traitement du json aussi.
                        String currencyValueFromKraken = "X" + currencyFrom + "Z" + currencyTo;
                        JToken selectSpecificNodeContent = JObject.Parse(json)["result"][currencyValueFromKraken];
                        String jsonfinal = selectSpecificNodeContent.ToString();

                        var currencyObject = JsonConvert.DeserializeObject<TickerItem>(jsonfinal);
                        Debug.Assert((currencyObject.askInfo) != null);

                        // Il faut faire qq chose pour plus avoir à taper dans l'index de la liste comme ça.
                        string currentValue = currencyObject.askInfo.ElementAt(0);
                        return Convert.ToDouble(currentValue, new NumberFormatInfo());
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
                WebResponse response = httpGetRequest("https://api.kraken.com/0/public/Time");
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = null;
                    json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        var jsonObject = JsonConvert.DeserializeObject<ServerItem.ServerObject>(json);
                        Debug.WriteLine(" --------------- ");
                        Debug.WriteLine(jsonObject.result);
                        Debug.WriteLine(jsonObject.result.rfc);
                        Debug.WriteLine(jsonObject.result.unixtime);
                        Debug.WriteLine(" --------------- ");
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
    }
}