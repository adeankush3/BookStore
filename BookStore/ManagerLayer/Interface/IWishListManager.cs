using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IWishListManager
    {
        Task<WishListModel> AddToWishList(WishListModel wishList);
        Task<bool> RemoveWishList(WishListModel wishList);
        IEnumerable<WishListModel> GetAllWishList();
    }
}
