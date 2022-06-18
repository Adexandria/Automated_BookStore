using Bookstore.Model.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Order
{
    public class OrderDTO
    {
        public Guid TrackingNumber { get; set; }
        public OrderStatus Status { get; set; }
        public List<CartDTO> Carts { get; set; }
    }
}
