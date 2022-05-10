using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{    
    [Route("auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IUsersBusiness _usersBusiness;
        public AuthController(IAuthBusiness authBusiness, IUsersBusiness usersBusiness)
        {
            _authBusiness = authBusiness;
            _usersBusiness = usersBusiness;
        }

        /// <summary>
        /// Authenticates registered users info.
        /// </summary>
        /// <returns>Returns a JwtToken and a status Message</returns>
        /// <remarks>
        /// Indicate user info as described below..
        /// 
        /// Sample request:
        /// 
        ///     POST / LOGIN
        ///     {
        ///         "email": "User@email.com",  *Required
        ///         "password": "ExamplePassword"  *Required
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a new JwtToken and a Status message</response>
        /// <response code="400">If any field is null</response>
        /// <response code="404">If the user is not found in database</response>
        /// <response code="500">If the server fails</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromForm] LoginDto login)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userLogged = _authBusiness.LoginUser(login);
                if (!(userLogged.Succeeded))
                {
                    return NotFound(userLogged);
                }
                return Ok(userLogged);
            }
            catch (Exception ex)
            {
                var listError = new string[]{ ex.Message };
                return StatusCode(500, new Response<String>(data: null, succeeded: false, errors: listError,message: "Error"));
            }
        }
        /// <summary>
        /// Creates an User.
        /// </summary>
        /// <returns>User created, sends email and makes login</returns>
        /// <remarks>
        /// Indicate new user info as described below.
        /// 
        /// Sample request:
        /// 
        ///     POST / Register
        ///     {
        ///         "firstName": "UserName",  *Required
        ///         "lastName": "UserLastName",  *Required
        ///         "email": "NewUser@email.com",  *Required
        ///         "password": "SuperSafePassword",  *Required
        ///         "photo": "image.png"  *Nullable
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Creates the user, sends email and makes login (returns JwtToken)</response>
        /// <response code="400">If any required field is null or if exists an user with the same email</response>
        /// <response code="500">If the server fails</response>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userRegisteredResult = await _usersBusiness.InsertUser(registerDto);
                if (!(userRegisteredResult.Succeeded))
                {
                    return BadRequest(userRegisteredResult);
                }
                return Ok(userRegisteredResult);
            }
            catch (Exception ex)
            {
                var listError = new string[] { ex.Message };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError, message: "Error"));
            }
        }

        /// <summary>
        /// Get current user info details.
        /// </summary>
        /// <returns>Current user info details</returns>
        /// <remarks>
        /// Use this endpoint when user is logged. No parameters needed.        
        /// </remarks>
        /// <response code="200">When logged, returns current user info details.</response>
        /// <response code="400">If the user is not logged.</response>
        [HttpGet]
        [Route("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _usersBusiness.GetMe();
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[] 
                { 
                    e.Message
                };
                
                return BadRequest(new Response<UserOutDTO>(null, succeeded: false, listErrors, message: "User not logged"));
            }
        }
    }
}