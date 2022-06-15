using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model
{
    public class Author
    {
        [Key]
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
