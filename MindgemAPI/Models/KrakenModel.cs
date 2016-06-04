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

        // Constructeur
        public KrakenModel()
        {
            // ...
        }

        // Récupération du cours de l'Ether via l'API Kraken
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
                        // A voir si on simplifie et augmente la complexité ou non.
                        String currencyValueFromKraken = "X" + currencyFrom + "Z" + currencyTo;
                        var jsonSelectResultCurrency = JObject.Parse(json)["result"][currencyValueFromKraken];
                        String jsonfinal = jsonSelectResultCurrency.ToString();
                        var currencyObject = JsonConvert.DeserializeObject<TickerItem>(jsonfinal);
                        Debug.Assert((currencyObject.askInfo) != null);
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