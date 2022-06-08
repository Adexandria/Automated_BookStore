using Bookstore.Model;
using Bookstore.Service;
using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Bookstore.Test
{
    public class DetailTest
    {
        readonly IDetail _detail;
        public DetailTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = Bookstore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._detail = new DetailRepository(db);
        }
        [Fact]
        public void AddDetail_Test()
        {
            BookDetail detail = new BookDetail
            {
                Description = "kdjinks",
                PublicationDate = DateTime.Now,
                Pages = 203,
                Sales = 0,
                ISBN13 = "1234",
                Publisher = "sjsk",
                Quantity = 100,
                Rating = 0
            };
            int insertedRow = _detail.AddDetail(detail).Result;
            Assert.Equal(1, insertedRow);
        }

        [Fact]
        public void UpdateDetail_Test()
        {
            BookDetail detail = new BookDetail
            {
                DetailId = Guid.Parse("074ee3be-205a-4f29-8a99-9a451e973cb4"),
                Description = "kdjinks",
                PublicationDate = DateTime.Now,
                Pages = 203,
                Sales = 1,
                ISBN13 = "1234",
                Publisher = "sjsk",
                Quantity = 99,
                Rating = 0
            };
            int updatedRow = _detail.UpdateDetail(detail).Result;
            Assert.Equal(1, updatedRow);
        }

        [Fact]
        public void DeleteDetail_Test()
        {
            int deletedRow = _detail.DeleteDetailById(Guid.Parse("3da62a39-d000-42d5-93c4-f03c937bc7eb")).Result;
            Assert.Equal(1, deletedRow);
        }
    }
}
