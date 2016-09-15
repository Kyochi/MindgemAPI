using MindgemAPI.dataobjects.kraken.publicdata;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MindgemAPI.utils
{
    public class UrlBuilder
    {
        public static List<String> serviceList = new List<string>() { "kraken", "poloniex", "bittrex" };
        public KrakenPropertiesItem kpi = new KrakenPropertiesItem();
        public UrlBuilder()
        {

        }

        // Gérée le cas ou les paramètres sont null ou bullshité et mettre à jour les tests en conséquence.
        public String getPairCode(String service, String from, String to)
        {
            if (service == null )
            {
                throw new exception.UrlBuilderException("Le service est null");
            }
            else
            {
                if (serviceList.Contains(service)) {
                    switch (service)
                    {
                        case "kraken":
                            return "X" + from + "Z" + to;
                        case "poloniex":
                            return from + "_" + to;
                        case "bittrex":
                            return from + "-" + to;
                        default:
                            return "API service not found";
                    }
                } else
                {
                    return String.Empty;
                }
            }
        }

        public String privateKrakenURLBuilder(String operation)
        {
            String urltotest = kpi.URL_PRIVATE_KRAKEN + operation;
            if (checkPrivateURL(urltotest))
            {
                return urltotest;
            }
            else return null;
        }

        private Boolean checkPrivateURL(String url)
        {
            Regex expressionToEvaluate = new Regex(@"^https:\/\/api\.kraken\.com\/[\d]\/private\/[a-zA-Z]*[^\/]$");
            return expressionToEvaluate.IsMatch(url);
        }
    }
}