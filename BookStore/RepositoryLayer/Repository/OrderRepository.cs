using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<OrderModel> Order;
        private readonly IConfiguration configuration;

        public OrderRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            Order = database.GetCollection<OrderModel>("Order");
        }

        public async Task<OrderModel> AddToOrder(OrderModel order)
        {
            try
            {
                var check = await this.Order.Find(x => x.orderID == order.orderID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Order.InsertOneAsync(order);
                    return order;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteOrder(OrderModel order)
        {
            try
            {
                var ifExists = await this.Order.FindOneAndDeleteAsync(x => x.orderID == order.orderID);
                return true;

            }
            catch (ArgumentNullException e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetAllOrder()
        {
            return Order.Find(FilterDefinition<OrderModel>.Empty).ToList();
        }
    }
}
