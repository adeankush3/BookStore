using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class CartManager :ICartManager
    {

        private readonly ICartRepository repo;
        public CartManager(ICartRepository repo)
        {
            this.repo = repo;
        }

        public async Task<CartModel> AddToCart(CartModel cart)
        {
            try
            {
                return await this.repo.AddToCart(cart);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
