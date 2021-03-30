using dev.groceries.lltm.local.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev.groceries.lltm.local.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroceriesController : ControllerBase
    {
        private readonly GroceriesContext Groceries;

        private readonly ILogger<GroceriesController> _logger;

        public GroceriesController(ILogger<GroceriesController> logger)
        {
            _logger = logger;
            this.Groceries = new GroceriesContext();
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            return await Task.Run(() => {
                this._logger.LogInformation("Received GET");
                return this.Groceries.itemCollection.ToArray();
            });
        }

        [HttpPost]
        public async Task<Item> Post(Item item)
        {
            return await Task.Run(() => {
                this._logger.LogInformation("Inserting a new grocery item");
                this.Groceries.Add(item);
                this.Groceries.SaveChanges();

                return item;
            });
        }

        [HttpPut]
        async public Task<Item> Put(Item item)
        {
            return await Task.Run(() => {
                Console.WriteLine("Updating the given grocery item");

                Item foundItem = this.Groceries.itemCollection.Where(t => t.Id == item.Id).ToList().First() as Item;
                foundItem.Name = item.Name;
                foundItem.Count = item.Count;
                foundItem.Purchased = item.Purchased;
                this.Groceries.SaveChanges();

                return foundItem;
            });
        }

        [HttpDelete]
        public StatusCodeResult Delete(Item item)
        {
            Console.WriteLine("Updating the given grocery item");

            try
            {
                Item foundItem = this.Groceries.itemCollection.Where(t => t.Id == item.Id).ToList().First() as Item;
                this.Groceries.itemCollection.Remove(foundItem);
                this.Groceries.SaveChanges();

                return this.Ok();
            }
            catch (Exception exception)
            {
                // noop
                this._logger.LogError(exception.Message, exception.StackTrace.ToArray());
            }

            return this.BadRequest();
        }

        [HttpPatch]
        public async Task<Item> SetPurchased(Item item)
        {
            Console.WriteLine("Updating to set it purchased");

            item.Purchased = DateTime.Now;

            return await this.Put(item);
        }

    }
}
