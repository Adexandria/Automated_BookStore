using Bookstore.Model;
using Bookstore.Model.DTO.Address;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/{userId}/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        readonly IAddress _addressDb;
        public AddressController(IAddress _addressDb)
        {
            this._addressDb = _addressDb;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<AddressDTO>> GetUserAddress(Guid userId)
        {
            IEnumerable<Address> addresses = _addressDb.GetAddressByProfileId(userId);
            IEnumerable<AddressDTO> mappedAddresses = addresses.Adapt<IEnumerable<AddressDTO>>();
            return Ok(mappedAddresses);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAddress(Guid userId,AddressCreate newAddress)
        {
            Address address = newAddress.Adapt<Address>();
            await _addressDb.AddAddress(address, userId);
            return Ok("Created Successfully");

        }
        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateUserAddress(Guid userId,Guid addressId,AddressUpdate updatedAddress)
        {
            Address currentAddress = await _addressDb.GetAddress(addressId,userId);
            if(currentAddress == null)
            {
                return NotFound();
            }
            Address address = updatedAddress.Adapt<Address>();
            await _addressDb.UpdateAddress(address,userId);
            return Ok("Currently Successfully");
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteUserAddress(Guid userId,Guid addressId)
        {
            Address currentAddress = await _addressDb.GetAddress(addressId, userId);
            if (currentAddress == null)
            {
                return NotFound();
            }
            await _addressDb.DeleteAddressById(addressId, userId);
            return Ok("Deleted Successfully");
        }
    }
}
