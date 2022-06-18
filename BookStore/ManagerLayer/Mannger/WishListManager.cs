using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class WishListManager : IWishListManager
    {
        private readonly IWishListRepository repo;
        public WishListManager(IWishListRepository repo)
        {
            this.repo = repo;
        }

        public async Task<WishListModel> AddToWishList(WishListModel wishList)
        {
            try
            {
                return await this.repo.AddToWishList(wishList);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WishListModel> GetAllWishList()
        {
            try
            {
                return this.repo.GetAllWishList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RemoveWishList(WishListModel wishList)
        {
            try
            {
                return await this.repo.RemoveWishList(wishList);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
