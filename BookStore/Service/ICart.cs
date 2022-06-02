using Bookstore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service
{
    public interface ICart
    {
        Task<int> AddToCart(OrderCart cart);
        Task<int> UpdateCart(OrderCart cart);
        Task<int> DeleteFromCart(Guid cartId);
    }
}
