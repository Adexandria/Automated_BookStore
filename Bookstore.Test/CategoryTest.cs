using Bookstore.Model;
using Bookstore.Service;
using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Bookstore.Test
{
    public class CategoryTest
    {
        readonly ICategory _category;
        public CategoryTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._category = new CategoryRepository(db);
        }

        [Fact]
        public void AddCategory_Test()
        {
            BookCategory category = new BookCategory 
            { 
                Faculty = "Fcaulty of Science",
                Level = 3
            };
            int insertedRow = _category.AddCategory(category).Result;
            Assert.Equal<int>(1,insertedRow);
        }

        [Fact]
        public void UpdateCategory_Test()
        {
            BookCategory category = new BookCategory
            {
                CategoryId = Guid.Parse("8c9e8b7f-5f0f-44b5-8fe2-6cf9b15edb3b"),
                Faculty = "Fcaulty of Science",
                Level = 3
            };
            int updatedRow = _category.UpdateCategory(category).Result;
            Assert.Equal(1, updatedRow);
        }
        [Fact]
        public void DeleteCategory_Test()
        {
            int deletedRow = _category.DeleteCategoryById(Guid.Parse("e8175d95-8a2e-49b4-b49e-a85657a697a6")).Result;
            Assert.Equal(1, deletedRow);
        }
    }
}
