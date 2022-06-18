using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Bookstore.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Bookstore.Service;
using Xunit;

namespace Bookstore.Test
{
    public class OrderTest
    {
        readonly IOrder _order;
        public OrderTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._order = new OrderRepository(db);
        }

        [Fact]
        public void AddOrder_Test()
        {
            Order order = new Order
            {
                ProfileId = Guid.Parse("6bbeb8cb-72ce-460c-b2cc-33ff1f510b33"),
                Status = OrderStatus.NotPaid
            };
            int insertedRow = _order.AddOrder(order).Result;
            Assert.Equal(1, insertedRow);
        }

        [Fact]
        public void UpdateOrder_Test()
        {
            Order order = new Order
            {
                OrderId = Guid.Parse("52d59a50-9578-4dd1-81de-fc4192610968"),
                ProfileId = Guid.Parse("6bbeb8cb-72ce-460c-b2cc-33ff1f510b33"),
                Status = OrderStatus.Processing
            };
            int updatedRow = _order.UpdateOrder(order).Result;
            Assert.Equal(1, updatedRow);
        }

      /*  [Fact]
        public void DeleteOrder_Test() 
        {
            int deletedRow = _order.DeleteOrderById(Guid.Parse("cfa4c243-e5ac-4d4a-bbd1-a11c8309ea3b")).Result;
            Assert.Equal(1, deletedRow);
        }
*/
        [Fact]
        public void GetUserOrders_Test()
        {
            IEnumerable<Order> GetUserOrder = _order.GetUserOrders(Guid.Parse("6bbeb8cb-72ce-460c-b2cc-33ff1f510b33"));
            Assert.NotEmpty(GetUserOrder);
        }

        [Fact]
        public void GetordersByStatus_Test()
        {
            IEnumerable<Order> orders = _order.GetOrdersByStatus(0);
            Assert.NotEmpty(orders);
        }
        [Fact]
        public void GetOrderByTrackingNumber_Test()
        {
            Order order = _order.GetOrdersByTrackingNumber(Guid.Parse("52d59a50-9578-4dd1-81de-fc4192610968")).Result;
            Assert.NotNull(order);
        }
    }
}
