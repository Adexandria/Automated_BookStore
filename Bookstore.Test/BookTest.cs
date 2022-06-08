using Bookstore.Model;
using Bookstore.Service;
using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bookstore.Test
{

   public class BookTest
   {
        readonly IBook _book;
        public BookTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._book = new BookRepository(db);
        }
        [Fact]
        public void AddBook_Test()
        {
            Book book = new Book
            {
                Name = "First Book",
                BookLink = "sjs snishn",
                CategoryId = Guid.Parse("8c9e8b7f-5f0f-44b5-8fe2-6cf9b15edb3b"),
                DetailId = Guid.Parse("074ee3be-205a-4f29-8a99-9a451e973cb4"),
                Picture = "snfslfjj",
                Price = 1500
            };
            int insertRow = _book.AddBook(book).Result;
            Assert.Equal(1, insertRow);
        }
        [Fact]
        public void GetBookById_Test()
        {
            Book currentbook = _book.GetBookById(Guid.Parse("491ab2f3-cf1e-407b-b52e-70be84e74174")).Result;
            Assert.NotNull(currentbook);
        }

        [Fact]
        public void GetAllBooks_Test()
        {
            IEnumerable<Book> books = _book.GetBooks;
            Assert.NotEmpty(books);
        }

        [Fact]
        public void GetAllBooksByName_Test()
        {
            IEnumerable<Book> books = _book.GetBookByName("First Book");
            Assert.NotEmpty(books);
        }
        [Fact]
        public void GetAllBooksByFaculty_Test()
        {
            IEnumerable<Book> books = _book.GetBooksByFaculty("Fcaulty of Science");
            Assert.NotEmpty(books);
        }

        [Fact]
        public void GetAllBooksByAuthor_Test()
        {
            IEnumerable<Book> books = _book.GetBooksByAuthor("491ab2f3-cf1e-407b-b52e-70be84e74174");
            Assert.Empty(books);
        }

        [Fact]
        public void GetAllBooksByLevel_Test()
        {
            IEnumerable<Book> books = _book.GetBooksByLevel("Fcaulty of Science", 3);
            Assert.NotEmpty(books);
        }

        [Fact]
        public void GetBookByISBN()
        {
            Book book = _book.GetBookByISBN13("1234").Result;
            Assert.NotNull(book);
        }
        [Fact]
        public void UpdateBook_Test()
        {
            Book book = new Book
            {
                BookId = Guid.Parse("491ab2f3-cf1e-407b-b52e-70be84e74174"),
                Name = "First Book",
                BookLink = "sjs snishn",
                CategoryId = Guid.Parse("8c9e8b7f-5f0f-44b5-8fe2-6cf9b15edb3b"),
                DetailId = Guid.Parse("074ee3be-205a-4f29-8a99-9a451e973cb4"),
                Picture = "snfslfjj",
                Price = 2500
            };
            int updatedRow = _book.UpdateBook(book).Result;
            Assert.Equal(1, updatedRow);

        }
        [Fact]
        public void DeleteBook_Test()
        {
            int deletedRow = _book.DeleteBookById(Guid.Parse("491ab2f3-cf1e-407b-b52e-70be84e74174")).Result;
            Assert.Equal(1, deletedRow);
        }
    }
}
