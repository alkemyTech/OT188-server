using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {

        private readonly ISlidesBusiness _slidesBusiness;
        public SlidesController(ISlidesBusiness slidesBusiness)
        {
            _slidesBusiness = slidesBusiness;
        }

        /// GET: slides
        /// <summary>
        ///    Get Slides list.
        /// </summary>
        /// <response code="200">OK: Returns slides list.</response> 
        /// <response code="400">BadRequest: Could not retrieve data.</response>    
        /// <response code="401">Unauthorized: Invalid Token or not provided.</response>
        /// <response code="500">Error: Internal server error</response> 
        [ProducesResponseType(typeof(Response<IEnumerable<SlideDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<SlideDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var result = await _slidesBusiness.GetSlides(true);
                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }

        /// <summary>
        /// Get category by id (details)
        /// </summary>
        /// <param name="id">ID of the slide row</param>
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetailSlideDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var result = await _slidesBusiness.GetDetailSlide(id);
                if(result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }

        /// POST: slides
        /// <summary>
        ///     Method to create a new Slide.
        /// </summary>
        /// <remarks>
        ///     Adds a new slide row to the db.
        /// </remarks>
        /// <param name="add">New Member object (dto).</param>
        /// <response code="200">OK: Returns a response with the dto object</response>        
        /// <response code="400">BadRequest: Failed to create slide.</response>          
        /// <response code="500">Error: Internal server error</response> 
        /// <response code="401">BadRequest: Could not retrieve data.</response>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AddSlideDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromForm] AddSlideDTO add)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _slidesBusiness.Add(add);
                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }

        /// PUT: slides
        /// <summary>
        ///     Method to update a slide.
        /// </summary>
        /// <remarks>
        ///     Updates the slide register in the db.
        /// </remarks>
        /// <param name="data">member item with new information (dto).</param>
        /// <param name="id">ID of the member to update.</param>
        /// <response code="200">OK: Returns a response with updated info</response>        
        /// <response code="400">BadRequest: Failed to update member.</response>          
        /// <response code="500">Error: Internal server error</response>  
        /// <response code="401">BadRequest: Could not retrieve data.</response>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response<DetailSlideDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<DetailSlideDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromForm] UpdateSlideDTO data, int id)
        {
            try
            {
                if (data.Image == null &&
                    data.Order == null &&
                    data.OrganizationId == null &&
                    data.Text == null)
                {
                    return BadRequest("Indique por lo menos un campo a modificar");
                }
                var result = await _slidesBusiness.Update(data, id);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                var error = new Response<string>(e.Message, false, null, "Server Error");
                return StatusCode(500, error);
            }
        }

        /// DELETE: slides
        /// <summary>
        ///     Method to delete a Slide.
        /// </summary>
        /// <remarks>
        ///     Deletes a slide row from db.
        /// </remarks>
        /// <param name="id">ID of the slide to delete.</param>
        /// <response code="200">OK: Returns a success response.</response>
        /// <response code="400">BadRequest: Failed to delete the slide.</response>
        /// <response code="500">Error: Internal Server Error.</response>
        /// <response code="401">Unauthorized: Invalid Token or not provided.</response> 
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _slidesBusiness.Delete(id);

                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }
    }
}
