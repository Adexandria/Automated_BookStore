using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class BookRepository : IBook
    {
        readonly DbService db;
        public BookRepository(DbService db)
        {
            this.db = db;

        }
        public IEnumerable<Book> GetBooks 
        {
            get
            {
                return db.Books.OrderBy(s => s.BookId);
            }
        }


        public async Task<int> AddBook(Book book)
        {
            try
            {
                book.BookId = Guid.NewGuid();
                await db.Books.AddAsync(book);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Book> GetBookById(Guid bookId)
        {
            return await db.Books.Where(s => s.BookId == bookId).FirstOrDefaultAsync();
        }

        public IEnumerable<Author> GetAuthorByBookId(Guid bookId)
        {
            return  db.BookAuthors.Include(s => s.Author).Where(s => s.BookId == bookId).Select(s => s.Author);
        }
        public async Task<Book> GetBookByISBN(string isbn)
        {
            Guid detailId = await db.BookDetails.Where(s => s.ISBN13 == isbn).Select(s => s.DetailId).FirstOrDefaultAsync();
            return await db.Books.Where(s => s.DetailId == detailId).FirstOrDefaultAsync();
        }

        public IEnumerable<Book> GetBookByName(string bookName)
        {
            return db.Books.Where(s => s.Name == bookName).OrderBy(s => s.BookId);
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            return db.BookAuthors.Include(s => s.Book).Include(s=>s.Author).Where(s => s.Author.Name.Contains(author)).Select(s => s.Book);
        }

        public IEnumerable<Book> GetBooksByFaculty(string faculty)
        {
            return db.Books.Include(s => s.Category).Where(s => s.Category.Faculty == faculty);
        }

        public IEnumerable<Book> GetBooksByLevel(string department, int level)
        {
            return db.Books.Include(s => s.Category).Where(s => s.Category.Department == department).Where(s=>s.Category.Level == level);
        }

        public async Task<int> UpdateBook(Book updatedBook)
        {
            try
            {
                Book currentBook = await GetBookById(updatedBook.BookId);
                db.Entry(currentBook).CurrentValues.SetValues(updatedBook);
                db.Entry(currentBook).State = EntityState.Modified;
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<int> DeleteBookById(Guid bookId)
        {
            try
            {
                Book currentBook = await GetBookById(bookId);
                db.Books.Remove(currentBook);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }

        
    }
}
