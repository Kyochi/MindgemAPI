using Microsoft.WindowsAzure.Mobile.Service;
using MindgemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MindgemAPI.Controllers
{
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

        //Pour acceder à ça : http://localhostIX.../api/mindgem/
        public String getEthereumPrice()
        {
            // Test GH 3
            return Convert.ToString(this.kModel.getCurrentEtherPrice("ETH", "EUR"));
        }
    }
}
