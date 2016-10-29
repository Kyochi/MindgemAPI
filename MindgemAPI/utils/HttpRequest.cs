using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace MindgemAPI.utils
{
    public class HttpRequest
    {
        public static WebResponse getRequest(String url, String contentType = "application/json; charset=utf-8")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = contentType;
            return request.GetResponse();
        }
        public static WebResponse postRequest(String url, Dictionary<String, String> headers, Dictionary<String, String> dataPost, String contentType = "application/x-www-form-urlencoded")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;
            request.Method = "POST";
            request.ContentType = contentType;

            String postDataQuery = HttpRequest.buildHttpQuery(dataPost);
            Byte[] dataContent = HttpRequest.postDataToBytes(postDataQuery);
            request.ContentLength = dataContent.Length;

            foreach (var elementToAdd in headers)
            {
                request.Headers.Add(elementToAdd.Key + elementToAdd.Value);
            }

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(dataContent, 0, dataContent.Length);
            dataStream.Flush();
            dataStream.Close();

            return request.GetResponse();
        }
        public static Byte[] postDataToBytes(String postData)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetBytes(postData);
        }
        public static String buildHttpQuery(Dictionary<String, String> dataPostToQuery)
        {
            StringBuilder sBuilder = new StringBuilder();
            foreach (var elementOfDict in dataPostToQuery)
            {
                sBuilder.Append(HttpUtility.UrlEncode(elementOfDict.Key));
                sBuilder.Append("=");
                sBuilder.Append(HttpUtility.UrlEncode(elementOfDict.Value));
                sBuilder.Append("&");
            }
            sBuilder.Remove((sBuilder.Length) - 1, 1);

            return sBuilder.ToString();
        }


    }
}