﻿using MindgemAPI.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            String nonceGenerate = encoder.generateNonce();
            Byte[] signature = this.getSignature(url, privateApiKey, nonceGenerate, otpPass, null);

            Dictionary<String, String> genericsHeaders = getHeader(apiKey, signature);

            Dictionary<String, String> postDataDict = new Dictionary<string, string>();
            postDataDict.Add("nonce", nonceGenerate);

            WebResponse response = utils.HttpRequest.postRequest(url, genericsHeaders, postDataDict);
            String json = null;

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                json = reader.ReadToEnd();
            }

            return json;
        }
        public Dictionary<String, String> getHeader(String apikeyHead, Byte[] sign)
        {
            Dictionary<String, String> headers = null;
            if (apikeyHead != null && sign != null)
            {
                headers = new Dictionary<String, String>();
                headers.Add("API-Key: ", apikeyHead);
                headers.Add("API-Sign: ", Convert.ToBase64String(sign));
            }
            return headers;
        }
        public Byte[] getSignature(String url, String privatekey, String nonce, String otpPwd,  Dictionary<String, String> additionalPostData)
        {
            var uri = new Uri(url);
            String path = uri.PathAndQuery;
            Byte[] signature = null;

            Dictionary<String, String> postDataDict = new Dictionary<string, string>();
            postDataDict.Add("nonce", nonce);
            if (otpPwd != null)
            {
                postDataDict.Add("otp", otpPwd);
            }

            String postDataString = utils.HttpRequest.buildHttpQuery(postDataDict);
            String noncePostData = postDataDict["nonce"] + postDataString;
            Byte[] hashSha256 = encoder.sha256_hash(noncePostData);
            Byte[] bytePath = Encoding.UTF8.GetBytes(path);

            Byte[] fusionByteArray = new byte[bytePath.Length + hashSha256.Length];
            bytePath.CopyTo(fusionByteArray, 0);
            hashSha256.CopyTo(fusionByteArray, bytePath.Length);

            signature = encoder.hashMac_sha512(fusionByteArray, privatekey);
            return signature;
        }
    }
}