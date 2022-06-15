using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookstore.Model;

namespace Bookstore.Service.Interface
{
    public interface IBook
    {
        Task<Book> GetBookById(Guid bookId);
        IEnumerable<Author> GetAuthorByBookId(Guid bookId);
        IEnumerable<Book> GetBooks { get; }
        IEnumerable<Book> GetBookByName(string bookName);
        Task<Book> GetBookByISBN(string isbn);
        IEnumerable<Book> GetBooksByFaculty(string faculty);
        IEnumerable<Book> GetBooksByLevel(string department, int level);
        IEnumerable<Book> GetBooksByAuthor(string author);
        Task<int> AddBook(Book book);
        Task<int> UpdateBook(Book updatedBook);
        Task<int> DeleteBookById(Guid bookId);

    }
}