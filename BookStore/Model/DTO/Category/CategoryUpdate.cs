using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Category
{
    public class CategoryUpdate
    {
        public Guid CategoryId { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public int Level { get; set; }
    }
}
