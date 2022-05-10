using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesBusiness _activitiesService;

        public ActivitiesController(IActivitiesBusiness activitiesService)
        {
            _activitiesService = activitiesService;
        }
        /// <summary>
        /// Create an Activity.
        /// </summary>
        /// <returns>
        /// Returns a JwtToken and a status Message</returns>
        /// <remarks>
        /// provide activity information as described below..
        /// 
        /// Sample request:
        /// 
        ///     POST / ACTIVITY
        ///     {
        ///         "name": "name string",  *Required
        ///         "content": "content string"  *Required
        ///         "image":  file image string($binary)
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a new JwtToken and a Status message</response>
        /// <response code="400">If any field is null</response>
        /// <response code="404">If the activity is not found </response>
        /// <response code="500">If the server fails</response>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromForm] NewActivityDto activityDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _activitiesService.InsertActivity(activityDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[]{ e.Message};
                return StatusCode(500, new Response<NewActivityDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
        /// <summary>
        /// Update Activity. 
        /// </summary>
        /// <returns>
        /// Returns a JwtToken and a status Message</returns>
        /// <remarks>
        /// provide activity information as described below
        /// 
        /// Sample request:
        /// 
        ///     UPDATE/ ACTIVITY
        ///     {
        ///         "name": "name string",  
        ///         "content": "content string"  
        ///         "image":  file image string($binary)
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a new JwtToken and a Status message</response>
        /// <response code="400">If any field is null  or not exist entity</response>

        /// <response code="500">If the server fails</response>
        [HttpPut("{id}")]
        [Authorize(Roles ="Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromForm] UpdateActivityDTO data, int id)
        {
            try
            {
                if (data.Name == null && data.Content == null && data.Image == null)
                {
                    return BadRequest("Ingrese por lo menos un campo a modificar");
                }
                var result = await _activitiesService.UpdateActivity(data, id);
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
