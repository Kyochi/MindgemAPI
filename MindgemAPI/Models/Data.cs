using MindgemAPI.DataObjects;
using Newtonsoft.Json;
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
    public class Data
    {

        public String nameAccount { get; set; }
        public int currentEtherPrice { get; set; }


        public Double getcurrentEtherPrice(String dataUrl)
        {
            try
            {
                WebResponse response = httpGetRequest(dataUrl);
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    String json = null;
                    json = reader.ReadToEnd();
                    if (!String.IsNullOrEmpty(json))
                    {
                        var jsonObject = JsonConvert.DeserializeObject<JsonModelClass.RootObject>(json);
                        Debug.Assert((jsonObject.result.XETHZEUR.a) != null);
                        String currentValue = jsonObject.result.XETHZEUR.a.ElementAt(0);
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

        private WebResponse httpGetRequest(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";
            return request.GetResponse();
        }
    }
}