using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface IAddress
    {
        Task<int> AddAddresss(Address address, Guid profileId);
        Task<int> UpdateAddress(Address address);
        Task<int> DeleteAddressById(Guid addressId);
    }
}
