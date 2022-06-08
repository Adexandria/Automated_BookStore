using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class AuthorRepository : IAuthor
    {
        readonly DbService db;
        public AuthorRepository(DbService db)
        {
            this.db = db;
        }
        public async Task<int> AddAuthor(Author author)
        {
            try
            {
                author.AuthorId = Guid.NewGuid();
                await db.Authors.AddAsync(author);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteAuthorById(Guid authorId)
        {
            try
            {
                Author currentAuthor = await GetAuthor(authorId);
                db.Authors.Remove(currentAuthor);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateAuthor(Author updatedAuthor)
        {
            try
            {
                Author currentAuthor = await GetAuthor(updatedAuthor.AuthorId);
                db.Entry(currentAuthor).CurrentValues.SetValues(updatedAuthor);
                db.Entry(currentAuthor).State = EntityState.Modified;
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<Author> GetAuthor(Guid authorId)
        {
            return await db.Authors.Where(s => s.AuthorId == authorId).FirstOrDefaultAsync();
        }
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
    }
}
