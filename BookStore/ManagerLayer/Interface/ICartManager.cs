using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface ICartManager
    {
        Task<CartModel> AddToCart(CartModel cart);
        Task<bool> RemoveCart(CartModel cart);
    }
}
