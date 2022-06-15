using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model
{
    public class BookCategory
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
    }
}