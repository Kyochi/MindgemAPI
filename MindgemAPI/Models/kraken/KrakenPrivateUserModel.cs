using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MindgemAPI.Models.kraken
{
    public class KrakenPrivateUserModel
    {
        public utils.Encoder encoder = new utils.Encoder();
        public UrlBuilder urlBuilder;
        public DataObjectProvider dataObjectProvider;
        public KrakenPrivateUserModel()
        {
            urlBuilder = new UrlBuilder();
            dataObjectProvider = new DataObjectProvider();
        }
        public String getPrivateData(String operationType, String apiKey, String privateApiKey, String otpPass = null)
        {
            String url = urlBuilder.privateKrakenURLBuilder(operationType);
            String signature = this.getSignature(url, privateApiKey, otpPass, null);

            Dictionary<String, String> genericsHeaders = getHeader(apiKey, signature);
            WebResponse response = utils.HttpRequest.postRequest(url, genericsHeaders, null);
            String json = null;

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                json = reader.ReadToEnd();
            }

            return json;
        }
        public Dictionary<String, String> getHeader(String apikeyHead, String sign)
        {
            Dictionary<String, String> headers = null;
            if (apikeyHead != null && sign != null)
            {
                headers = new Dictionary<String, String>();
                headers.Add("API-Key: ", apikeyHead);
                headers.Add("API-Sign: ", encoder.Base64Encode(sign));
            }
            return headers;
        }
        public String getSignature(String url, String privatekey, String otpPwd, Dictionary<String, String> additionalPostData)
        {
            var uri = new Uri(url);
            String path = uri.PathAndQuery;
            String signature = null;

            String nonce = encoder.generateNonce();
            Dictionary<String, String> postDataDict = new Dictionary<string, string>();
            postDataDict.Add("nonce", nonce);
            if (otpPwd != null)
            {
                postDataDict.Add("otp", otpPwd);
            }
            String postDataString = utils.HttpRequest.buildHttpQuery(postDataDict);
            String hashSha256 = encoder.sha256_hash(postDataDict["nonce"] + postDataString);

            signature = encoder.hashMac_sha512(path + hashSha256, encoder.Base64Decode(privatekey));
            return signature;
        }
    }
}