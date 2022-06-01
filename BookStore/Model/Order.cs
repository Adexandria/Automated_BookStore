using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Model
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("UserProfile")]
        public Guid ProfileId { get; set; }
        [ForeignKey("OrderCart")]
        public Guid CartId { get; set; }
        public OrderCart OrderCart { get; set; }
        public OrderStatus Status { get; set; }
    }
}
