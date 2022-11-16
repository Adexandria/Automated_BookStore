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
    [Route("api/Order/{orderId}/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        readonly ICart _cartDb;
        readonly IOrder _orderDb;
        public CartsController(ICart _cartDb, IOrder _orderDb)
        {
            this._cartDb = _cartDb;
            this._orderDb = _orderDb;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CartDTO>> GetOrderCart(Guid orderId)
        {
            IEnumerable<OrderCart> carts = _cartDb.GetOrdersCart(orderId);
            IEnumerable<CartDTO> mappedCarts = carts.Adapt<IEnumerable<CartDTO>>();
            return Ok(mappedCarts);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid orderId, CartCreate newCart)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OrderCart cart = newCart.Adapt<OrderCart>();

            if (await _orderDb.CheckUserOrder(Guid.Parse(userId)))
            {
                Order order = new OrderCreate { ProfileId = Guid.Parse(userId) }.Adapt<Order>();
                await _orderDb.AddOrder(order);
            }
            
            await _cartDb.AddToCart(orderId, cart);
            return Ok("Created Successfully");
        }
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateItemCart(Guid orderId,Guid bookId,CartUpdate updatedCart)
        {
            OrderCart currentCart = await _cartDb.GetCartByBookId(orderId, bookId);
            if(currentCart == null)
            {
                return NotFound();
            }
            OrderCart cart = updatedCart.Adapt<OrderCart>();
            await _cartDb.UpdateCart(orderId, bookId, cart);
            return Ok("updated successfully");

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(Guid orderId,Guid cartId)
        {
            OrderCart currentCart = await _cartDb.GetCart(orderId, cartId);
            if (currentCart == null)
            {
                return NotFound();
            }
            await _cartDb.DeleteFromCart(orderId, cartId);
            return Ok("Deleted successfully");
            }
    }
}
