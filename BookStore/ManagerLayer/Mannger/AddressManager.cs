using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Mannger
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository repo;
        public AddressManager(IAddressRepository repo)
        {
            this.repo = repo;
        }

        public async Task<AddressModel> AddToAddress(AddressModel address)
        {
            try
            {
                return await this.repo.AddToAddress(address);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AddressModel> GetAllAddress()
        {
            try
            {
                return this.repo.GetAllAddress();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RemoveAddress(AddressModel address)
        {
            try
            {
                return await this.repo.RemoveAddress(address);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            try
            {
                return await this.repo.UpdateAddress(address);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
