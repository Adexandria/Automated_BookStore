using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Bookstore.Service;
using Bookstore.Model;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Test
{
    public class AddressTest
    {
        readonly IAddress _address;
        public AddressTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._address = new AddressRepository(db);
        }

       [Fact]
        public void GetAddress_Test()
        {
            Guid profileId = Guid.Parse("6bbeb8cb-72ce-460c-b2cc-33ff1f510b33");
            Address address = _address.GetAddressByProfileId(profileId).Result;
            Assert.NotNull(address);
        }

        [Fact]
        public void AddAddress_Test()
        {
            Guid profileId = Guid.Parse("6bbeb8cb-72ce-460c-b2cc-33ff1f510b33");
            Address address = new Address
            {
                AdressBox = "jjsosj",
                City = "nss",
                State = "indkjosdn",
                StreetNo = "snojd"
            };
            int insertedRow = _address.AddAddresss(address, profileId).Result;
            Assert.Equal(1, insertedRow);
        }
        [Fact]
        public void UpdateAddress_Test()
        {
            Address address = new Address
            {
                AddressId = Guid.Parse("609614f3-1124-4444-820f-8b217667460f"),
                AdressBox = "jjsosj",
                City = "nss",
                State = "indkjosdn",
                StreetNo = "snojd",
                ProfileId = Guid.Parse("6bbeb8cb-72ce-460c-b2cc-33ff1f510b33")
            };
            int updatedRow = _address.UpdateAddress(address).Result;
            Assert.Equal(1, updatedRow);
        }

        [Fact]
        public void DeleteAddress_Test()
        {
            int deletedRow = _address.DeleteAddressById(Guid.Parse("609614f3-1124-4444-820f-8b217667460f")).Result;
            Assert.Equal(1, deletedRow);
        }
    }
}
