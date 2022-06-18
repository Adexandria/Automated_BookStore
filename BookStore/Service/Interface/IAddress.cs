using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface IAddress
    {
        Task<Address> GetAddress(Guid addressId,Guid profileId);
        IEnumerable<Address> GetAddressByProfileId(Guid profileId);
        Task<int> AddAddress(Address address, Guid profileId);
        Task<int> UpdateAddress(Address address,Guid profileId);
        Task<int> DeleteAddressById(Guid addressId,Guid profileId);
    }
}
