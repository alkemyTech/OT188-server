using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;

namespace OngProject.Controllers
{
    [Route("organization")]
    [ApiController]
    
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _organizationsBusiness;

        public OrganizationsController(IOrganizationsBusiness organizationsBusiness)
        {
            _organizationsBusiness = organizationsBusiness;
        }

        /// GET: organization
        /// <summary>
        ///    Get Organizations list.
        /// </summary>
        /// <response code="200">OK: Returns Organization list.</response>  
        /// <response code="400">BadRequest: Failed to retrieve Organizations list.</response>
        /// <response code="500">Error: Internal server error</response> 
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<OrganizationDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<IEnumerable<OrganizationDTO>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        [Route("public")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _organizationsBusiness.GetOrganizations(true);
                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, message: "Server Error"));
            }
        }

        /// <summary>
        /// Get Organization by id (details)
        /// </summary>
        /// <param name="id">ID of the organization row</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {                
                var result = await _organizationsBusiness.GetOrganization(id);

                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }           
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, message: "Server Error"));
            }
        }

        /// PUT: organization
        /// <summary>
        ///     Method to update an organization.
        /// </summary>
        /// <remarks>
        ///     Updates the organization register in the db.
        /// </remarks>
        /// <param name="upOrg">organization item with new information (dto).</param>
        /// <response code="200">OK: Returns a response with updated info</response>        
        /// <response code="400">BadRequest: Failed to update organization.</response>          
        /// <response code="500">Error: Internal server error</response>  
        [HttpPut]
        [Route("public")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put([FromForm] UpdateOrganizationDTO upOrg)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _organizationsBusiness.UpdateOrganizations(upOrg);

                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[] { e.Message };
                return StatusCode(500, new Response<NewActivityDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
    }
}