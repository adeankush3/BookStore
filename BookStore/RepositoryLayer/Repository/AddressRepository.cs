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
    public class AddressRepository : IAddressRepository
    {
        private readonly IMongoCollection<AddressModel> AddressType;
        private readonly IConfiguration configuration;

        public AddressRepository(IDBSetting db, IConfiguration configuration)
        {

            this.configuration = configuration;
            var userclient = new MongoClient(db.ConnectionString);
            var database = userclient.GetDatabase(db.DatabaseName);
            AddressType = database.GetCollection<AddressModel>("AddressType");
        }

        public async Task<AddressModel> AddToAddress(AddressModel address)
        {
            try
            {
                var check = await this.AddressType.Find(x => x.addressID == address.addressID).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this.AddressType.InsertOneAsync(address);
                    return address;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public async Task<bool> RemoveAddress(AddressModel address)
        {
            try
            {
                await this.AddressType.FindOneAndDeleteAsync(x => x.addressID == address.addressID);
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            {
                try
                {
                    var ifExists = await this.AddressType.Find(x => x.addressID == address.addressID).SingleOrDefaultAsync();
                    if (ifExists != null)
                    {
                        await this.AddressType.UpdateOneAsync(x => x.addressID == address.addressID,
                            Builders<AddressModel>.Update.Set(x => x.fullAddress, address.fullAddress)
                            .Set(x => x.city, address.city)
                            .Set(x => x.state, address.state)
                            .Set(x => x.pinCode, address.pinCode));
                        return ifExists;

                    }
                    else
                    {
                        await this.AddressType.InsertOneAsync(address);
                        return address;
                    }
                }
                catch (ArgumentNullException e)
                {

                    throw new Exception(e.Message);
                }
            }
        }

        IEnumerable<AddressModel> IAddressRepository.GetAllAddress()
        {
            return AddressType.Find(FilterDefinition<AddressModel>.Empty).ToList();
        }
    }
}
