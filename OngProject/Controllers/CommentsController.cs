using Microsoft.AspNetCore.Authorization;
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
    [Route("comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsBusiness _commentsBusiness;

        public CommentsController(ICommentsBusiness commentsBusiness)
        {
            _commentsBusiness = commentsBusiness;
        }

        /// DELETE: comments
        /// <summary>
        ///     Method to delete a Comment.
        /// </summary>
        /// <remarks>
        ///     Deletes a comment row from db.
        /// </remarks>
        /// <param name="Id">ID of the comment to delete.</param>
        /// <response code="200">OK: Returns a success response.</response>
        /// <response code="500">Error: Internal Server Error.</response>
        [HttpDelete("{Id}"), Authorize]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = await _commentsBusiness.DeleteComments(Id);
                if (result.Succeeded == false)
                {
                    return StatusCode(403, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response<string>()
                {
                    Data = "Error - 404",
                    Message = ex.Message,
                    Succeeded = false
                };
                return StatusCode(404, response);
            }
        }


        /// POST: members
        /// <summary>
        ///     Method to create a new Member.
        /// </summary>
        /// <remarks>
        ///     Adds a new member row to the db.
        /// </remarks>
        /// <param name="newCommentDto">New comment object (dto).</param>
        /// <response code="200">OK: Returns a response with the dto object</response>        
        /// <response code="400">BadRequest: Failed to create comment.</response>          
        /// <response code="500">Error: Internal server error</response>  
        [HttpPost]
        [ProducesResponseType(typeof(Response<NewCommentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewCommentDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<NewCommentDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert(NewCommentDto newCommentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _commentsBusiness.InsertComment(newCommentDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var listError = new string[] { ex.Message };
                return StatusCode(500, new Response<NewCommentDto>(data: null, succeeded: false, errors: listError, message: "Error"));

            }
        }


        /// PUT: member
        /// <summary>
        ///     Method to update a comment.
        /// </summary>
        /// <remarks>
        ///     Updates the comment register in the db.
        /// </remarks>
        /// <param name="request">comment item with new information (dto).</param>
        /// <response code="200">OK: Returns a response with updated info</response>        
        /// <response code="400">BadRequest: Failed to update comment.</response>          
        /// <response code="500">Error: Internal server error</response>
        /// <param name="id">ID of the comment to delete.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCommentDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = await _commentsBusiness.UpdateComment(id, request);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new Response<string>(e.Message, false, null, "Server Internal Error");
                return StatusCode(500, error);
            }
        }
    }
}
