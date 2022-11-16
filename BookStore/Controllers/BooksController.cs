using Bookstore.App.Service.Interface;
using Bookstore.Model;
using Bookstore.Model.DTO.Author;
using Bookstore.Model.DTO.Book;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly IBook _bookDb;
        readonly IBlob _blob;
        public BooksController(IBook _bookDb, IBlob _blob)
        {
            this._blob = _blob;
            this._bookDb = _bookDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BooksDTO>> GetBooks()
        {
            IEnumerable<Book> books = _bookDb.GetBooks;
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookDTO>> GetBookById(Guid bookId)
        {
            Book book = await _bookDb.GetBookById(bookId);
            IEnumerable<AuthorDTO> authors = _bookDb.GetAuthorByBookId(bookId).Adapt<IEnumerable<AuthorDTO>>();
            BookDTO mappedBook = book.Adapt<BookDTO>();
            mappedBook.Author = authors.Adapt<IEnumerable<AuthorDTO>>();
            return Ok(mappedBook);
        } 

        [HttpGet("names/search")]
        public ActionResult<IEnumerable<BooksDTO>> SearchBooksByName([FromQuery]string name)
        {
            IEnumerable<Book> books = _bookDb.GetBookByName(name);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);

        }

        [HttpGet("details/search")]
        public async Task<ActionResult<BookDTO>> SearchBookByISBN(string isbn)
        {
            Book book = await _bookDb.GetBookByISBN(isbn);
            BookDTO mappedBook = book.Adapt<BookDTO>();
            return Ok(mappedBook);
        }

        [HttpGet("categories/search")]
        public  ActionResult<IEnumerable<BooksDTO>> SearchBookByFaculty([FromQuery]string faculty)
        {
            IEnumerable<Book> books = _bookDb.GetBooksByFaculty(faculty);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [HttpGet("categories/search")]
        public ActionResult<IEnumerable<BooksDTO>> SearchBookByFaculty([FromQuery] string department, [FromQuery]int level)
        {
            IEnumerable<Book> books = _bookDb.GetBooksByLevel(department,level);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [HttpGet("authors/search")]
        public ActionResult<IEnumerable<BooksDTO>> SearchByAuthor([FromQuery] string author)
        {
            IEnumerable<Book> books = _bookDb.GetBooksByAuthor(author);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromForm]BookCreate newBook)
        {
            string bookLink = await _blob.Upload(newBook.Book);
            string picture = await _blob.Upload(newBook.Picture);
            Book book = newBook.Adapt<Book>();
            book.BookLink = bookLink;
            book.Picture = picture;
            book.CategoryId = await _bookDb.GetCategoryByDepartmentAndLevel(newBook.Department, newBook.Level);
            book.DetailId = await _bookDb.GetDetailByISBN(newBook.ISBN);
            await _bookDb.AddBook(book);
            return Ok(book.BookId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(Guid bookId,[FromForm] BookUpdate updatedBook)
        {
            string bookLink = await _blob.Upload(updatedBook.BookLink);
            string picture = await _blob.Upload(updatedBook.Picture);
            Book book = updatedBook.Adapt<Book>();
            book.BookLink = bookLink;
            book.Picture = picture;
            Book currentBook = await _bookDb.GetBookById(bookId);
            if(currentBook == null)
            {
                return NotFound();
            }
            await _bookDb.UpdateBook(book);
            return Ok("Updated Successfully");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(Guid bookId)
        {
            Book currentBook = await _bookDb.GetBookById(bookId);
            if (currentBook == null)
            {
                return NotFound();
            }
            await _bookDb.DeleteBookById(bookId);
            return Ok("Deleted Successfully");
        }
    }
}
