using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MindgemAPI.utils
{
    public class UrlBuilder
    {
        public static List<String> serviceList = new List<string>() { "kraken", "poloniex", "bittrex" };

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
    }
}