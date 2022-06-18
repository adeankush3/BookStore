using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IWishListRepository
    {
        Task<WishListModel> AddToWishList(WishListModel wishList);
        Task<bool> RemoveWishList(WishListModel wishList);
        IEnumerable<WishListModel> GetAllWishList();
    }
}
