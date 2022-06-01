using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Picture { get; set; }
        public string BookLink { get; set; }
        [ForeignKey("BookCategory")]
        public Guid CategoryId { get; set; }
        public BookCategory Category { get; set; }
        [ForeignKey("BookDetail")]
        public Guid DetailId { get; set; }
        public BookDetail Detail { get; set; }
    }
}
