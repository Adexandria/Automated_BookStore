using Bookstore.Model;
using Bookstore.Model.DTO.Cart;
using Bookstore.Model.DTO.Order;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        readonly IOrder _orderDb;
        readonly ICart _cartDb;
        public OrdersController(IOrder _orderDb, ICart _cartDb)
        {
            this._orderDb = _orderDb;
            this._cartDb = _cartDb;
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrdersDTO>> GetUserOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Order> orders = _orderDb.GetUserOrders(Guid.Parse(userId));
            IEnumerable<OrderDTO> mappedOrders = orders.Adapt<IEnumerable<OrderDTO>>();
            return Ok(mappedOrders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetUserOrder(Guid orderId)
        {
            Order order = await _orderDb.GetOrdersByTrackingNumber(orderId);
            if(order == null)
            {
                return NotFound();
            }
            OrderDTO mappedOrder = order.Adapt<OrderDTO>();
            List<CartDTO> carts = _cartDb.GetOrdersCart(orderId).ToList().Adapt<List<CartDTO>>();
            mappedOrder.Carts = carts;
            return Ok(mappedOrder);
        }

        [HttpGet("status/search")]
        public ActionResult<IEnumerable<OrdersDTO>> SearchOrderByStatus(int status)
        {
            IEnumerable<Order> orders = _orderDb.GetOrdersByStatus(status);
            IEnumerable < OrderDTO > mappedOrders = orders.Adapt<IEnumerable<OrderDTO>>();
            return Ok(mappedOrders);
        }
        
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(Guid orderId,OrderUpdate updateOrder)
        {
            Order currentOrder = await _orderDb.GetOrdersByTrackingNumber(orderId);
            if (currentOrder == null)
            {
                return NotFound();
            }
            Order order = updateOrder.Adapt<Order>();
            await _orderDb.UpdateOrder(order);
            return Ok("Updated Successfully");
        }
    }
}
