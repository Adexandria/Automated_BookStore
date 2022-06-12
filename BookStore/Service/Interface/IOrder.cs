using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface IOrder
    {
        IEnumerable<Order> GetUserOrders(Guid profileId);
        IEnumerable<Order> GetOrdersByStatus(int status);
        Task<Order> GetOrdersByTrackingNumber(Guid trackingNumber);

        Task<int> AddOrder(Order order);
        Task<int> UpdateOrder(Order order);
        Task<int> DeleteOrderById(Guid trackingNumber);
        

    }
}
