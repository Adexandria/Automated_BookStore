using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class BookAuthorRepository : IBookAuthor
    {
        readonly DbService _db;
        public BookAuthorRepository(DbService _db)
        {
            this._db = _db;
        }
        public async Task<int> AddBookAuthor(BookAuthor bookAuthor)
        {
            try
            {
                bookAuthor.Id = Guid.NewGuid();
                await _db.BookAuthors.AddAsync(bookAuthor);
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteBookAuthor(Guid id)
        {
            try
            {
                BookAuthor currentBookAuthor = await GetBookAuthor(id);
                _db.BookAuthors.Remove(currentBookAuthor);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateBookAuthor(BookAuthor bookAuthor)
        {
            try
            {
                BookAuthor currentBookAuthor = await GetBookAuthor(bookAuthor.Id);
                _db.Entry(currentBookAuthor).CurrentValues.SetValues(bookAuthor);
                _db.Entry(currentBookAuthor).State = EntityState.Modified;
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task<BookAuthor> GetBookAuthor(Guid id)
        {
            return await _db.BookAuthors.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
