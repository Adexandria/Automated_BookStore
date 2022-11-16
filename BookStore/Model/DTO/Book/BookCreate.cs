using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Book
{
    public class BookCreate
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public IFormFile Picture { get; set; }
        public IFormFile Book { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
        public string ISBN { get; set; }
    }
}
