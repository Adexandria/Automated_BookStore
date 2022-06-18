using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Model.DTO.Order
{
    public class OrdersDTO
    {
        public Guid TrackingNumber { get; set; }
        public OrderStatus Status { get; set; }
    }
}
