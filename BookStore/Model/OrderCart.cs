using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model
{
    public class OrderCart
    {
        [Key]
        public Guid CartId { get; set; }
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("UserProfile")]
        public Guid ProfileId { get; set; }
    }
}
