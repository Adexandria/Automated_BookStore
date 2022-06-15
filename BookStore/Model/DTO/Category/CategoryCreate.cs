using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Category
{
    public class CategoryCreate
    {
        [Required(ErrorMessage ="Enter Faculty")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "Enter Department")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Level")]
        public int Level { get; set; }
    }
}
