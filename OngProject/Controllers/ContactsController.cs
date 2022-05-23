using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    //[Authorize(Roles = "Administrator")]

    public class ContactsController : ControllerBase
    {
        private readonly IContactsBusiness _contactsBusiness;

        public ContactsController(IContactsBusiness contactsBusiness)
        {
            _contactsBusiness = contactsBusiness;
        }

        /// GET: contacts
        /// <summary>
        ///    Get contacts list.
        /// </summary>
        /// <response code="200">OK: Returns members paginated list.</response>  
        /// <response code="401">Unauthorized: Invalid Token or not provided.</response>
        /// <response code="500">Error: Internal server error</response> 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contactsList = await _contactsBusiness.GetContacts(true);

                if (contactsList == null)
                {
                    return NotFound(new Response<string>(data: null, succeeded: false, errors: null, message: "Not Found"));
                }

                return Ok(new Response<IEnumerable<ContactDto>>(data:contactsList,succeeded:true,errors:null,message:"Success"));
            }
            catch (Exception ex)
            {
                var listError = new string[] { ex.Message };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError, message: "Ha ocurrido un error al intentar realizar la operación"));
            }
        }

        /// POST: contacts
        /// <summary>
        ///     Method to create a new Contact.
        /// </summary>
        /// <remarks>
        ///     Adds a new contact row to the db.
        /// </remarks>
        /// <param name="dto">New Member object (dto).</param>
        /// <response code="200">OK: Returns a response with the dto object</response>        
        /// <response code="400">BadRequest: Failed to create member.</response>          
        /// <response code="500">Error: Internal server error</response>  
        [HttpPost]
        [ProducesResponseType(typeof(Response<RegisterContactDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<RegisterContactDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
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
                var listError = new string[] { ex.Message };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError, message: "Ha ocurrido un error al intentar realizar la operación"));
            }
        }
    }
}