using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICartRepository
    {
        Task<CartModel> AddToCart(CartModel cart);
        Task<bool> RemoveCart(CartModel cart);
        Task<CartModel> UpdateCartQuantity(CartModel cart);
        IEnumerable<CartModel> GetAllCart();
    }
}
