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
    public class WishListRepository : IWishListRepository
    {
        private readonly IMongoCollection<WishListModel> Wishlist;
        private readonly IConfiguration configuration;

        public WishListRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            Wishlist = database.GetCollection<WishListModel>("Wishlist");
        }

        public async Task<WishListModel> AddToWishList(WishListModel wishList)
        {
            try
            {
                var check = await this.Wishlist.Find(x => x.wishListID == wishList.wishListID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.Wishlist.InsertOneAsync(wishList);
                    return wishList;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public async Task<bool> RemoveWishList(WishListModel wishList)
        {
            try
            {
                await this.Wishlist.FindOneAndDeleteAsync(x => x.wishListID == wishList.wishListID);
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        IEnumerable<WishListModel> IWishListRepository.GetAllWishList()
        {
            return Wishlist.Find(FilterDefinition<WishListModel>.Empty).ToList();
        }
    }
}
