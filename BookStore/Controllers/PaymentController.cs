using Bookstore.App.Service;
using Bookstore.Model;
using Bookstore.Model.DTO.Profile;
using Bookstore.Model.Payment;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore.App.Controllers
{
    [Route("api/Orders/{orderId}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : ControllerBase
    {
        readonly ICart _cartDb;
        readonly IProfile _profileDb;
        readonly BookstoreService _service;
        public PaymentController(BookstoreService _service, ICart _cartDb, IProfile _profileDb)
        {
            this._service = _service;
            this._cartDb = _cartDb;
            this._profileDb = _profileDb;
        }

        ///<param name="userId">
        ///the user's id
        ///</param>
        ///<param name="orderId">
        ///the user's order id
        ///</param>
        /// <summary>
        /// Pay for order
        /// </summary>
        /// 
        /// 
        /// <returns>A string status</returns>
        [HttpPost]
        public async Task<ActionResult> PayCharge(Guid orderId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _service.GetSecrets();

                ProfileDTO currentUser = _profileDb.GetProfile(Guid.Parse(userId)).Result.Adapt<ProfileDTO>();
                List<OrderCart> carts = _cartDb.GetOrdersCart(orderId).ToList();

                int price = _service.GetPrice(carts);
                var charge = _service.SetCharge(price, currentUser.Email);

                var pendingCharge = await _service.InitializeCharge(charge);
                var verification = JsonConvert.DeserializeObject<Verification>(pendingCharge);
                var pin = _service.CreatePin(verification.Data.Reference);

                var content = await _service.SubmitPin(pin);

                var status = JsonConvert.DeserializeObject<Reciept>(content).Data.Status;
                if (status == "success")
                {
                    await _service.UpdateOrder(orderId, OrderStatus.Processing);
                    await _service.UpdateItem(carts);
                    return Ok("Successful, Your order is been processed");
                }
                return BadRequest("Try Again");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
