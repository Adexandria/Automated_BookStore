using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class CartRepository : ICart
    {
        readonly DbService db;
        public CartRepository(DbService db)
        {
            this.db = db;
        }
        public async Task<int> AddToCart(OrderCart cart)
        {
            try
            {
                cart.CartId  = Guid.NewGuid();
                await db.Carts.AddAsync(cart);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteFromCart(Guid cartId)
        {
            try
            {
                OrderCart currentCart = await GetCart(cartId);
                db.Carts.Remove(currentCart);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<OrderCart> GetOrdersCart(Guid orderId)
        {
            return db.Carts.Where(s => s.OrderId == orderId).Include(s => s.Book).OrderBy(s => s.BookId);
        }
        public async Task<int> UpdateCart(OrderCart cart,Guid bookId)
        {
            try
            {
                OrderCart currentCart = await GetCartByBookId(bookId);
                db.Entry(currentCart).CurrentValues.SetValues(cart);
                db.Entry(currentCart).State = EntityState.Modified;
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async Task<OrderCart> GetCartByBookId(Guid bookId)
        {
            var x = await db.Carts.Where(s => s.BookId == bookId).Include(s => s.Order).Where(s => s.Order.Status == OrderStatus.NotPaid)
                .FirstOrDefaultAsync();
            return x;
        }
        private async Task<OrderCart> GetCart(Guid cartId)
        {
            return await db.Carts.Where(s => s.CartId == cartId).FirstOrDefaultAsync();
        }
        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }
    }
}
