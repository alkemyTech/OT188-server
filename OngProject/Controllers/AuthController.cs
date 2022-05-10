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
        /// Indicate email and password.
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
        /// <response code="400">Returns a Bad Request message</response>
        /// <response code="404">Returns a Not Found with a response class message</response>
        /// <response code="500">Returns an Internal server error with a response class</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] LoginDto login)
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

        [HttpPost]
        [Route("register")]
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

        [HttpGet]
        [Route("me")]
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