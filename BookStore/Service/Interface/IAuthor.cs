using System;
using Bookstore.Model;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface IAuthor
    {
        Task<int> AddAuthor(Author author);
        Task<int> UpdateAuthor(Author updatedAuthor);
        Task<int> DeleteAuthorById(Guid authorId);

    }
}