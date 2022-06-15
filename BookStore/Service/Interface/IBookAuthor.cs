using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface IBookAuthor
    {
        Task<int> AddBookAuthor(BookAuthor bookAuthor);
        Task<int> UpdateBookAuthor(BookAuthor bookAuthor);
        Task<int> DeleteBookAuthor(Guid id);
    }
}
