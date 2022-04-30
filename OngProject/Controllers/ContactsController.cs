using Microsoft.AspNetCore.Authorization;
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

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RegisterContactDto dto)
        {
            var response = await _contactsBusiness.InsertAsync(dto);

            return response.Errors == null ? Ok(response) : StatusCode(500, response);                    
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }

        [Route("{id}")]
        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            return NoContent();
        }
    }
}