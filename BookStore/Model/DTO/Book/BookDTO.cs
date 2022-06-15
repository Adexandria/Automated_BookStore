using Bookstore.Model.DTO.Author;
using Bookstore.Model.DTO.Category;
using Bookstore.Model.DTO.Detail;
using System.Collections.Generic;

namespace Bookstore.Model.DTO.Book
{
    public class BookDTO
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Picture { get; set; }
        public string BookLink { get; set; }        
        public CategoryDTO Category { get; set; }
        public DetailDTO Detail { get; set; }
        public IEnumerable<AuthorDTO> Author { get; set; } 
       
    }
}
