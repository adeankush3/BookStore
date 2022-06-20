using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepository
    {
        Task<AddressModel> AddToAddress(AddressModel address);
        Task<bool> RemoveAddress(AddressModel address);
        Task<AddressModel> UpdateAddress(AddressModel address);
        IEnumerable<AddressModel> GetAllAddress();
    }
}
