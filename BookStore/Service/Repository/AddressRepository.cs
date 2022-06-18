using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class AddressRepository : IAddress
    {
        readonly DbService db;
        public AddressRepository(DbService db)
        {
            this.db = db;
        }
        public async Task<int> AddAddress(Address address, Guid profileId)
        {
            try
            {
                address.AddressId = Guid.NewGuid();
                address.ProfileId = profileId;
                await db.Addresses.AddAsync(address);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<int> DeleteAddressById(Guid addressId,Guid profileId)
        {
            try
            {
                Address currentAddress = await GetAddress(addressId,profileId);
                db.Addresses.Remove(currentAddress);
                return await SaveChanges();
            }
            catch ( Exception ex)
            {

                throw ex;
            }
           

        }

        public IEnumerable<Address> GetAddressByProfileId(Guid profileId)
        {
            return  db.Addresses.Where(s => s.ProfileId == profileId);
        }

        public async Task<int> UpdateAddress(Address address,Guid profileId)
        {
            try
            {
                Address currentAddress = await GetAddress(address.AddressId,profileId);
                db.Entry(currentAddress).CurrentValues.SetValues(address);
                db.Entry(currentAddress).State = EntityState.Modified;
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Address> GetAddress(Guid addressId,Guid profileId)
        {
            return await db.Addresses.Where(s=>s.ProfileId == profileId).Where(s => s.AddressId == addressId).FirstOrDefaultAsync(); 
        }
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
    }
}
