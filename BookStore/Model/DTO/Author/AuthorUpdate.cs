using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Author
{
    public class AuthorUpdate
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
