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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<ProfileDTO>> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserProfile user = await _profileDb.GetProfile(Guid.Parse(userId));
            if (user == null)
            {
                return NotFound("This user doesn't exist");
            }
            ProfileDTO mappedProfile = user.Adapt<ProfileDTO>();
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            mappedProfile.Email = userEmail;
            return Ok(mappedProfile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(ProfileUpdate updatedProfile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserProfile currentUser = await _profileDb.GetProfile(Guid.Parse(userId));
            if (currentUser == null)
            {
                return NotFound("This user doesn't exist");
            }
            UserProfile user = updatedProfile.Adapt<UserProfile>();
            user.ProfileId = currentUser.ProfileId;
            await _profileDb.UpdateUserProfile(user);
            return Ok("Updated Successfully");
        }
        
    }
}
