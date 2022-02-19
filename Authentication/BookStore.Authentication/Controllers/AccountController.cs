using Authentication.Application.DTO;
using Authentication.Application.Interface;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Service;
using AutoMapper;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace BookStore.Authentication.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        readonly UserManager<SignUp> userManager;
        readonly IPasswordHasher<SignUp> passwordHasher;
        readonly SignInManager<SignUp> signInManager;
        readonly IMapper mapper;
        readonly EmailService emailService;
        private Credentials _credentials;


        public AccountController(SignInManager<SignUp> signInManager, UserManager<SignUp> userManager,
            IMapper mapper, IPasswordHasher<SignUp> passwordHasher, EmailService emailService, Credentials _credentials)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this._credentials = _credentials;
        }


        ///<param name="newUser">
        ///an object to sign up a user
        ///</param>
        /// <summary>
        /// Sign up a new user
        /// </summary>
        /// 
        /// <returns>201 response</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(SignUpCreate newUser)
        {
            try
            {
                SignUp user = mapper.Map<SignUp>(newUser);
                if (newUser.Password.Equals(newUser.ReTypePassword))
                {
                    user.UserName = user.Email;
                    IdentityResult identity = await userManager.CreateAsync(user, user.PasswordHash);
                    if (identity.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Student");
                        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Student"));
                        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, $"{user.Email}"));

                        string token = await EmailConfirmationToken(user);
                        Mail newMail = new Mail {
                            To = user.Email,
                            Text = $"Verify your BookStore Student Email using this token {token}"
                        };
                        
                        bool isSuccessful = await emailService.SendSimpleMessage(newMail);
                        if (isSuccessful)
                        {
                            return this.StatusCode(StatusCodes.Status201Created, $"Welcome,{user.First_Name} verify your email");
                        }
                        return BadRequest();
                    }
                    else
                    {
                        return BadRequest(identity.Errors);
                    }
                }
                return this.StatusCode(StatusCodes.Status400BadRequest, "Password not equal,retype password");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }




        ///<param name="email">
        ///\a user's email
        ///</param>
        ///<param name="token">
        ///a token to confirm user's email
        ///</param>
        /// <summary>
        ///email confrimation
        /// </summary>
        /// 
        /// <returns>200 response</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("emailconfirmation",Name ="EmailConfirmation")]
        public async Task<ActionResult> VerifyEmailToken([FromBody]Token token,string email)
        {
            try
            {
                SignUp currentUser = await GetUser(email);
                if (currentUser == null)
                {
                    return NotFound("username doesn't exist");
                }
                IdentityResult result = await userManager.ConfirmEmailAsync(currentUser, token.GeneratedToken);
                if (result.Succeeded)
                {
                    return this.StatusCode(StatusCodes.Status200OK, $"Welcome,{currentUser.UserName} Email has been verified");
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Token");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        ///<param name="user">
        ///an object to login
        ///</param>
        /// <summary>
        ///Login User
        /// </summary>
        /// 
        /// <returns>200 response</returns>

        //To generate the token to reset password
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login user)
        {
            try
            {
                SignUp logindetails = mapper.Map<SignUp>(user);
                SignUp currentUser = await GetUser(logindetails.Email);
                if (currentUser == null)
                {
                    return NotFound("username or password is not correct");
                }

                //This verfies the user password by using IPasswordHasher interface
                PasswordVerificationResult passwordVerifyResult = passwordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, user.Password);
                if (passwordVerifyResult.ToString() == "Success")
                {
                   
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), new Claim(ClaimTypes.Role,"Student") };
                    var identity = new ClaimsIdentity(claims,JwtBearerDefaults.AuthenticationScheme);

                    await signInManager.SignInWithClaimsAsync(currentUser, null, claims);

                    var signingCredentials = _credentials.GetSigningCredentials();
                    var tokenOptions = _credentials.GenerateTokenOptions(signingCredentials, claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(token);         
                }

                return BadRequest("username or password is not correct");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        /// <summary>
        ///sign out User
        /// </summary>
        /// 
        /// <returns>200 response</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [HttpPost("signout")]
        public async Task<ActionResult> SignOut()
        {
            try
            {
               await signInManager.SignOutAsync();
               return Ok("Signed out");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [NonAction]
        private async Task<string> EmailConfirmationToken(SignUp newUser)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(newUser);

        }

        [NonAction]
        private async Task<SignUp> GetUser(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

    }
}

