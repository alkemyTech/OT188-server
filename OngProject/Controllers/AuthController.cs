﻿using Microsoft.AspNetCore.Http;
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
                return Ok(_authBusiness.LoginUser(login));
            }
            catch (Exception ex)
            {
                return BadRequest("Ok:False - " + ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(RegisterDto registerDto)
        {
            try
            {
                return Ok(await _usersBusiness.InsertUser(registerDto));
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
                var listErrors = new string[2];
                listErrors[0] = e.Message;
                listErrors[1] = e.StackTrace.ToString();
                return BadRequest(new Response<UserOutDTO>(null, succeeded: false, listErrors, message: "User not logger"));
            }
        }
    }
}