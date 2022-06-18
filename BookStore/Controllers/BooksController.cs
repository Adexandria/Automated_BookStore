using Bookstore.Model;
using Bookstore.Model.DTO.Author;
using Bookstore.Model.DTO.Book;
using Bookstore.Service.Interface;
using Mapster;
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
        readonly IBookAuthor _bookAuthor;
        public BooksController(IBook _bookDb, IBookAuthor _bookAuthor)
        {
            this._bookAuthor = _bookAuthor;
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
            mappedBook = authors.Adapt<BookDTO>();
            return Ok(mappedBook);
        } 

        [HttpGet("search")]
        public ActionResult<IEnumerable<BooksDTO>> SearchBooksByName([FromQuery]string name)
        {
            IEnumerable<Book> books = _bookDb.GetBookByName(name);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);

        }

        [HttpGet("search/{isbn}")]
        public async Task<ActionResult<BookDTO>> SearchBookByISBN(string isbn)
        {
            Book book = await _bookDb.GetBookByISBN(isbn);
            BookDTO mappedBook = book.Adapt<BookDTO>();
            return Ok(mappedBook);
        }

        [HttpGet("search/category")]
        public  ActionResult<IEnumerable<BooksDTO>> SearchBookByFaculty([FromQuery]string faculty)
        {
            IEnumerable<Book> books = _bookDb.GetBooksByFaculty(faculty);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [HttpGet("search/category")]
        public ActionResult<IEnumerable<BooksDTO>> SearchBookByFaculty([FromQuery] string department, [FromQuery]int level)
        {
            IEnumerable<Book> books = _bookDb.GetBooksByLevel(department,level);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [HttpGet("search/author")]
        public ActionResult<IEnumerable<BooksDTO>> SearchByAuthor([FromQuery] string author)
        {
            IEnumerable<Book> books = _bookDb.GetBooksByAuthor(author);
            IEnumerable<BooksDTO> mappedBooks = books.Adapt<IEnumerable<BooksDTO>>();
            return Ok(mappedBooks);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookCreate newBook)
        {
            Book book = newBook.Adapt<Book>();
            await _bookDb.AddBook(book);
            return Ok("Created Successfully");
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(Guid bookId, BookUpdate updatedBook)
        {
            Book book = updatedBook.Adapt<Book>();
            Book currentBook = await _bookDb.GetBookById(bookId);
            if(currentBook == null)
            {
                return NotFound();
            }
            await _bookDb.UpdateBook(book);
            return Ok("Updated Successfully");
        }
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
