using Microsoft.WindowsAzure.Mobile.Service;
using MindgemAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MindgemAPI.Controllers
{
    [RoutePrefix("api/mindgem")]
    public class MindgemController : ApiController
    {
        public ApiServices Services { get; set; }
        
        public const uint MAXUSERS = 10;

        KrakenModel[] dataAccount;
        public KrakenModel kModel;

        // Constructeur
        public MindgemController()
        {
            kModel = new KrakenModel();
            dataAccount = new KrakenModel[MAXUSERS];
        }

        [Route("kraken/getprice/{from}/{to}")]
        public String getKrakenPrice(String from, String to)
        {
            return Convert.ToString(this.kModel.getCurrentKrakenPrice(from, to));
        }

        [Route("kraken/getservtime")]
        public String getServerTime()
        {
            return kModel.getServerTime();
        }

        [Route("kraken/trades/{from}/{to}")]
        public String getKrakenTrades(String from, String to)
        {
            return Convert.ToString(this.kModel.getTradesLastDay(from, to));
        }

    }
}
