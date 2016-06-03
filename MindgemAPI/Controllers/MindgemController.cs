using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using MindgemAPI.DataObjects;
using MindgemAPI.Models;
using System;

namespace MindgemAPI.Controllers
{
    public class MindgemController : TableController<WalletItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<WalletItem>(context, Request, Services);
        }

        public const uint MAXUSERS = 10;

        KrakenModel[] dataAccount;
        public KrakenModel kModel;

        // Constructeur
        public MindgemController()
        {
            kModel = new KrakenModel();
            dataAccount = new KrakenModel[MAXUSERS];
        }

        //Pour acceder à ça : http://localhost:3213/tables/mindgem/getethereumprice/
        public String getEthereumPrice()
        {
            return Convert.ToString(this.kModel.getCurrentEtherPrice("ETH", "EUR"));
        }

        /*
        // GET tables/TodoItem
        public IQueryable<TodoItem> GetAllTodoItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            TodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
        */
    }
}