using Bookstore.Model;
using Bookstore.Model.DTO.Address;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        readonly IAddress _addressDb;
        public AddressesController(IAddress _addressDb)
        {
            this._addressDb = _addressDb;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<AddressDTO>> GetUserAddress()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Address> addresses = _addressDb.GetAddressByProfileId(Guid.Parse(userId));
            IEnumerable<AddressDTO> mappedAddresses = addresses.Adapt<IEnumerable<AddressDTO>>();
            return Ok(mappedAddresses);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAddress(AddressCreate newAddress)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Address address = newAddress.Adapt<Address>();
            await _addressDb.AddAddress(address, Guid.Parse(userId));
            return Ok(address.AddressId);

        }
        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateUserAddress(Guid addressId,AddressUpdate updatedAddress)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            Address currentAddress = await _addressDb.GetAddress(addressId, Guid.Parse(userId));
            if(currentAddress == null)
            {
                return NotFound();
            }
            Address address = updatedAddress.Adapt<Address>();
            await _addressDb.UpdateAddress(address, Guid.Parse(userId));
            return Ok("Currently Successfully");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]
        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteUserAddress(Guid addressId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Address currentAddress = await _addressDb.GetAddress(addressId, Guid.Parse(userId));
            if (currentAddress == null)
            {
                return NotFound();
            }
            await _addressDb.DeleteAddressById(addressId, Guid.Parse(userId));
            return Ok("Deleted Successfully");
        }
    }
}
