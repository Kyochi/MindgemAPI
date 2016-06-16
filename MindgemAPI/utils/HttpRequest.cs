using System;
using System.Net;

namespace MindgemAPI.utils
{
    public class HttpRequest
    {
        // A spécialiser certainement
        public static WebResponse getRequest(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";
            return request.GetResponse();
        }
    }
}