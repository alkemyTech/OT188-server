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
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IUsersBusiness _usersBusiness;
        public AuthController(IAuthBusiness authBusiness, IUsersBusiness usersBusiness)
        {
            _authBusiness = authBusiness;
            _usersBusiness = usersBusiness;
        }
        [HttpPost]
        [Route("login")]
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
                var listError = new string[]
                {
                    ex.Message
                };
                return StatusCode(500, new Response<String>(data: null, succeeded: false, errors: listError));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(RegisterDto registerDto)
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
                return StatusCode(500, ex.Message);
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