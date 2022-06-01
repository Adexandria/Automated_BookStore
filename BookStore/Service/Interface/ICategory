using System;
using Bookstore.Model;
using System.Threading.Tasks;

public interface ICategory
{
    //IEnumerable<BookCategory> GetAllBookCategories{get;}
    Task<int> AddCategory(BookCategory category);
    Task<int> UpdateCategory(BookCategory updatedCategory);
    Task<int> DeleteCategoryById(Guid categoryId);
}