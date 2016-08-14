using MindgemAPI.dataobjects.bittrex.publicdata;
using MindgemAPI.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MindgemAPI.Models.bittrex
{
    public class BittrexPublicMarketModel
    {
        public BittrexPropertiesItem bpi = new BittrexPropertiesItem();
        public static Dictionary<String, System.Threading.Timer> loader = new Dictionary<string, System.Threading.Timer>();

        public static Dictionary<String, BittrexTickerItem> tickerItemPair = new Dictionary<string, BittrexTickerItem>();
        public static Dictionary<String, DateTime> tickerTime = new Dictionary<string, DateTime>();

        public UrlBuilder urlBuilder;
        public DataObjectProvider dataObjectProvider;

        public BittrexPublicMarketModel()
        {
            urlBuilder = new UrlBuilder();
            dataObjectProvider = new DataObjectProvider();
        }

        public String getJson(String operationType, String currencyFrom = "", String currencyTo = "")
        {
            try
            {
                String urlPoloniexApi = "Unknown url";
                switch (operationType)
                {
                    case "ticker":
                        string pair = urlBuilder.getPairCode("bittrex", currencyFrom, currencyTo);
                        urlPoloniexApi = bpi.URL_PUBLIC_TICKER_BITTREX + pair;
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
}