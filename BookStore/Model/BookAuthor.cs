using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model
{
    public class BookAuthor
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
