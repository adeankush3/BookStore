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
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<CartModel> Cart;
        private readonly IConfiguration configuration;

        public CartRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            Cart = database.GetCollection<CartModel>("Cart");
        }

        public async Task<CartModel> AddToCart(CartModel cart)
        {
            try
            {
                var check = await this.Cart.Find(x => x.cartID == cart.cartID).SingleOrDefaultAsync();
                if(check == null)
                {
                    await this.Cart.InsertOneAsync(cart);
                    return cart;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
