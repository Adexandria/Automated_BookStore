using Bookstore.Model;
using Bookstore.Model.DTO.Profile;
using Bookstore.Service.Interface;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ProfileController : ControllerBase
    {
        readonly IProfile _profileDb;

        public ProfileController(IProfile _profileDb)
        {
            this._profileDb = _profileDb;
        }
        [HttpGet]
        public async Task<ActionResult<ProfileDTO>> GetUserProfile(Guid userId)
        {
            UserProfile user = await _profileDb.GetProfile(userId);
            if (user == null)
            {
                return NotFound("This user doesn't exist");
            }
            ProfileDTO mappedProfile = user.Adapt<ProfileDTO>();
            return Ok(mappedProfile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(Guid userId,ProfileUpdate updatedProfile)
        {

            UserProfile currentUser = await _profileDb.GetProfile(userId);
            if (currentUser == null)
            {
                return NotFound("This user doesn't exist");
            }
            UserProfile user = updatedProfile.Adapt<UserProfile>();
            user.Id = currentUser.Id;
            user.ProfileId = currentUser.ProfileId;
            await _profileDb.UpdateUserProfile(user);
            return Ok("Updated Successfully");
        }
        
    }
}
