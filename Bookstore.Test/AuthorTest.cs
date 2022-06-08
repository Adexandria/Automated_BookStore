using Bookstore.Model;
using Bookstore.Service;
using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Bookstore.Test
{
    public class AuthorTest
    {
        readonly IAuthor _author;
        public AuthorTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._author = new AuthorRepository(db);
        }

        [Fact]
        public void AddAuthor_Test()
        {
            Author author = new Author
            {
                Biography = "I am in techs",
                Name = "Adeola",
                BookId = Guid.Parse("a05ce308-15bc-4a28-965b-178f2319b644")
            };
            int insertRow = _author.AddAuthor(author).Result;
            Assert.Equal(1, insertRow);
        }

        [Fact]
        public void UpdateAuthor_Test()
        {

            Author author = new Author
            {
                AuthorId = Guid.Parse("e2564563-2efb-4d32-b742-2f4cc0143419"),
                Biography = "I am in tech",
                Name = "Adeola",
                BookId = Guid.Parse("a05ce308-15bc-4a28-965b-178f2319b644")
            };
            int updatedRow = _author.UpdateAuthor(author).Result;
            Assert.Equal(1, updatedRow);
        }

        [Fact]
        public void DeleteAuthor_Test()
        {
            int deletedRow = _author.DeleteAuthorById(Guid.Parse("fdcaea5b-7234-48f0-89a1-6a8c836e4a34")).Result;
            Assert.Equal(1, deletedRow);
        }
    }
}
