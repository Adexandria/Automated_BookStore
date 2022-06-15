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
            IEnumerable<Book> Books = _bookDb.GetBooks;
            IEnumerable<BooksDTO> mappedBooks = Books.Adapt<IEnumerable<BooksDTO>>();
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
    }
}
