using System;
using Bookstore.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bookstore.Service.Interface
{
    public interface IAuthor
    {
        Task<Author> GetAuthor(Guid authorId);
        Task<int> AddAuthor(Author author);
        Task<int> UpdateAuthor(Author updatedAuthor);
        Task<int> DeleteAuthorById(Guid authorId);

    }
}