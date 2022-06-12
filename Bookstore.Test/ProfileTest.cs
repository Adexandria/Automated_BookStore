using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Bookstore.Model;
using Microsoft.EntityFrameworkCore;
using Bookstore.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Test
{
    public class ProfileTest
    {
        readonly IProfile _profile;
        public ProfileTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._profile = new ProfileRepository(db);
        }
        [Fact]
        public void AddProfile_Test()
        {
            UserProfile profile = new UserProfile
            {
                FirstName ="Adeola",
                MatriculationNumber="U2017/5570012",
                Department ="Computer Science",
                Level = 4,
                LastName ="Aderibigbe",
                Faculty ="Science",
                Id = "c0ad81a9-6197-4d67-bede-b70bc4a6a7d3"

            };
            int insertedRow = _profile.AddUserProfile(profile).Result;
            Assert.Equal(1, insertedRow);
        }

        [Fact]
        public void GetProfile_Test()
        {
            UserProfile profile = _profile.GetProfile("c0ad81a9-6197-4d67-bede-b70bc4a6a7d3").Result;
            Assert.NotNull(profile);
        }

        [Fact]
        public void UpdateProfile_Test()
        {
            UserProfile profile = new UserProfile
            {
                ProfileId = Guid.Parse("13c98c44-bfb6-41af-847f-29a71e2ff9f3"),
                FirstName = "Adeola",
                MatriculationNumber = "U2017/5570012",
                Department = "Computer Science",
                Level = 3,
                LastName = "Aderibigbe",
                Faculty = "Science",
                Id = "c0ad81a9-6197-4d67-bede-b70bc4a6a7d3"

            };
            int updatedRow = _profile.UpdateUserProfile(profile).Result;
            Assert.Equal(1, updatedRow);
        }
        [Fact]
        public void DeleteProfile_Test()
        {
            int deletedRow = _profile.DeleteUserProfile("c0ad81a9-6197-4d67-bede-b70bc4a6a7d3").Result;
            Assert.Equal(1, deletedRow);
        }
    }
}
