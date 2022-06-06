using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class CategoryRepository : ICategory
    {
        readonly DbService db;
        public CategoryRepository(DbService db)
        {
            this.db = db;
        }
        public async Task<int> AddCategory(BookCategory category)
        {
            try
            {
                category.CategoryId = Guid.NewGuid();
                await db.BookCategories.AddAsync(category);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteCategoryById(Guid categoryId)
        {
            try
            {
                BookCategory currentCategory = await GetCategory(categoryId);
                db.BookCategories.Remove(currentCategory);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateCategory(BookCategory updatedCategory)
        {
            try
            {
                BookCategory currentCategory = await GetCategory(updatedCategory.CategoryId);
                db.Entry(currentCategory).CurrentValues.SetValues(updatedCategory);
                db.Entry(currentCategory).State = EntityState.Modified;
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<BookCategory> GetCategory(Guid categoryId)
        {
            return await db.BookCategories.Where(s => s.CategoryId == categoryId).FirstOrDefaultAsync();
        }
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
    }
}
