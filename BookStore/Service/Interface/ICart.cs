using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface ICart
    {
        IEnumerable<OrderCart> GetOrderCart(Guid orderId);
        Task<int> AddToCart(OrderCart cart);
        Task<int> UpdateCart(OrderCart cart);
        Task<int> DeleteFromCart(Guid cartId);
    }
}
