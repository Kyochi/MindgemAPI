using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using MindgemAPI.DataObjects;
using MindgemAPI.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Web.Routing;
using System.Net.Http.Headers;

namespace MindgemAPI
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    
    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<WalletItem> todoItems = new List<WalletItem>
            {
                new WalletItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new WalletItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };

            foreach (WalletItem todoItem in todoItems)
            {
                context.Set<WalletItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

