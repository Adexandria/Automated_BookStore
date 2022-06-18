using Bookstore.Model;
using Bookstore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Service.Repository
{
    public class OrderRepository : IOrder
    {
        readonly DbService db;
        public OrderRepository(DbService db)
        {
            this.db = db;
        }
        public async Task<int> AddOrder(Order order)
        {
            try
            {
                order.OrderId = Guid.NewGuid();
                await db.Orders.AddAsync(order);
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      

        public IEnumerable<Order> GetOrdersByStatus(int status)
        {
            return db.Orders.Where(s => (int)s.Status == status).OrderBy(s=>s.OrderId);
        }

        public async Task<Order> GetOrdersByTrackingNumber(Guid trackingNumber)
        {
            return await db.Orders.Where(s => s.OrderId == trackingNumber).FirstOrDefaultAsync();
        }

        public IEnumerable<Order> GetUserOrders(Guid profileId)
        {
            return db.Orders.Where(s => s.ProfileId == profileId).OrderBy(s => s.OrderId);
        }

        public async Task<bool> CheckUserOrder(Guid profileId)
        {
            Order order = await db.Orders.Where(s => s.ProfileId == profileId).Where(s => s.Status == OrderStatus.NotPaid).FirstOrDefaultAsync();
            if(order == null)
            {
                return false;
            }
            return true;
        }

        public async Task<int> UpdateOrder(Order order)
        {
            try
            {
                Order currentOrder = await GetOrdersByTrackingNumber(order.OrderId);
                order.ProfileId = currentOrder.ProfileId;
                db.Entry(currentOrder).CurrentValues.SetValues(order);
                db.Entry(currentOrder).State = EntityState.Modified;
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        private async Task<int> SaveChanges()
        {
            return await db.SaveChangesAsync();
        }

       
    }
}
