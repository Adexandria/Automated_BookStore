using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Interface
{
    public interface ICart
    {
        IEnumerable<OrderCart> GetOrdersCart(Guid orderId);
        Task<OrderCart> GetCartByBookId(Guid orderId, Guid bookId);
        Task<OrderCart> GetCart(Guid orderId, Guid cartId);
        Task<int> AddToCart(Guid orderId,OrderCart cart);
        Task<int> UpdateCart(Guid orderId,Guid bookId,OrderCart cart);
        Task<int> DeleteFromCart(Guid orderId,Guid cartId);
    }
}
