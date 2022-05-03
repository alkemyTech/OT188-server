﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

    public class ContactsController : ControllerBase
    {
        private readonly IContactsBusiness _contactsBusiness;

        public ContactsController(IContactsBusiness contactsBusiness)
        {
            _contactsBusiness = contactsBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contactsList = await _contactsBusiness.GetContacts(true);

                if (contactsList == null)
                {
                    return NotFound();
                }

                return Ok(contactsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RegisterContactDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var response = await _contactsBusiness.InsertAsync(dto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}