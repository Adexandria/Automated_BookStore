using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Bookstore.Service;
using Bookstore.Model;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bookstore.Test
{
   public  class CartTest
   {
        readonly ICart _cart;
        public CartTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbService>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database = BookStore.Test;Integrated Security=True;Connect Timeout=30;");
            DbService db = new DbService(optionsBuilder.Options);
            this._cart = new CartRepository(db);
        }

        [Fact]
        public void GetOrderCart_Test()
        {
            IEnumerable<OrderCart> carts = _cart.GetOrdersCart(Guid.Parse("52d59a50-9578-4dd1-81de-fc4192610968"));
            Assert.NotEmpty(carts);
        }
        [Fact]
        public void AddOrderCart_Test()
        {
            OrderCart cart = new OrderCart
            {
                BookId = Guid.Parse("a05ce308-15bc-4a28-965b-178f2319b644"),
                OrderId = Guid.Parse("52d59a50-9578-4dd1-81de-fc4192610968"),
                Quantity = 1
            };
            int insertedRow = _cart.AddToCart(cart).Result;
            Assert.Equal(1, insertedRow);
        }

        [Fact]
        public void UpdateOrderCart_Test()
        {
            OrderCart cart = new OrderCart
            {
                BookId = Guid.Parse("a05ce308-15bc-4a28-965b-178f2319b644"),
                OrderId = Guid.Parse("52d59a50-9578-4dd1-81de-fc4192610968"),
                Quantity = 2,
                CartId = Guid.Parse("ab443966-6f92-48b3-b219-3d46c4e910f9")
            };
            int updatedRow = _cart.UpdateCart(cart).Result;
            Assert.Equal(1, updatedRow);
        }

        [Fact]
        public void DeleteOrderCart_Test()
        {
            int deletedRow = _cart.DeleteFromCart(Guid.Parse("7b3e5355-b55a-4ff8-be46-be67feeae04e")).Result;
            Assert.Equal(1, deletedRow);
        }
   }
}
