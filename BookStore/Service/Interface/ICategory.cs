using System;
using Bookstore.Model;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface ICategory
    {
        //IEnumerable<BookCategory> GetAllBookCategories{get;}
        Task<int> AddCategory(BookCategory category);
        Task<int> UpdateCategory(BookCategory updatedCategory);
        Task<int> DeleteCategoryById(Guid categoryId);
    }
}