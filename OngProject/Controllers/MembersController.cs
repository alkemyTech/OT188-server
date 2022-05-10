using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;
using OngProject.Core.Models.Pagination;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersBusiness _membersBusiness;

        public MembersController(IMembersBusiness membersBusiness)
        {
            _membersBusiness = membersBusiness;
        }

        /// GET: members
        /// <summary>
        ///    Get Members paginated list.
        /// </summary>
        /// <response code="200">OK: Returns members paginated list.</response>  
        /// <response code="401">Unauthorized: Invalid Token or not provided.</response>    
        /// <response code="401">BadRequest: Could not retrieve data.</response>    
        /// <response code="500">Error: Internal server error</response> 
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [ProducesResponseType(typeof(PagedListResponse<MemberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(PagedListResponse<MemberDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> GetAll([FromQuery] PagedListParams pagedParams)
        {
            try
            {
                var response = await _membersBusiness.GetMembers(pagedParams);
                if (!response.Succeeded)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var listError = new[] { e.Message };
                return StatusCode(500, new Response<string>(null, false, listError , "Error"));
            }
            
        }

        /// POST: members
        /// <summary>
        ///     Method to create a new Member.
        /// </summary>
        /// <remarks>
        ///     Adds a new member row to the db.
        /// </remarks>
        /// <param name="newMemberDTO">New Member object (dto).</param>
        /// <response code="200">OK: Returns a response with the dto object</response>        
        /// <response code="400">BadRequest: Failed to create member.</response>          
        /// <response code="500">Error: Internal server error</response>  
        [HttpPost]
        [ProducesResponseType(typeof(Response<MemberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<MemberDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromForm]NewMemberDTO newMemberDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _membersBusiness.InsertMember(newMemberDTO);
                if (response.Succeeded == false)
                {
                    return BadRequest(response);               
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[] { e.Message };
                return StatusCode(500, new Response<string>(null, false, listErrors, "Error"));
            }

            
        }

        /// DELETE: members/5
        /// <summary>
        ///     Method to delete a Member.
        /// </summary>
        /// <remarks>
        ///     Deletes a member row from db.
        /// </remarks>
        /// <param name="id">ID of the member to delete.</param>
        /// <response code="200">OK: Returns a success response.</response>
        /// <response code="400">BadRequest: Failed to delete the member.</response>
        /// <response code="500">Error: Internal Server Error.</response>
        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _membersBusiness.DeleteMember(id);
                if (response.Succeeded == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var listError = new string[] { e.Message };
                return StatusCode(500, new Response<string>(null, false, listError, "Error"));
            }
        }

        /// PUT: member
        /// <summary>
        ///     Method to update a member.
        /// </summary>
        /// <remarks>
        ///     Updates the member register in the db.
        /// </remarks>
        /// <param name="memberUpdate">member item with new information (dto).</param>
        /// <response code="200">OK: Returns a response with updated info</response>        
        /// <response code="400">BadRequest: Failed to update member.</response>          
        /// <response code="500">Error: Internal server error</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response<MemberDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<MemberDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm] NewMemberDTO memberUpdate)
        {
            try
            {
                var result = await _membersBusiness.UpdateMemberAsync(id, memberUpdate);
                return result.Succeeded == true ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(null, false, new string[] { e.Message }, "Server Error"));
            }
        }
    }
}
