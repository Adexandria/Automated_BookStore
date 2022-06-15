using System;
using Bookstore.Model;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface ICategory
    {
        Task<BookCategory> GetCategory(Guid categoryId);
        Task<int> AddCategory(BookCategory category);
        Task<int> UpdateCategory(BookCategory updatedCategory);
        Task<int> DeleteCategoryById(Guid categoryId);
    }
}