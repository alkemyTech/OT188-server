using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;

using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;
using OngProject.Entities;

namespace OngProject.Controllers
{

    [Route("testimonials")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsBusiness _business;

        public TestimonialsController(ITestimonialsBusiness business)
        {
            _business = business;
        }
        /// <summary>
        /// Get all testimonials (paginated)
        /// </summary>
        /// <remarks>Indicate the number and size of page</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] PagedListParams pagedParams)
        {
            try
            {
                var response = await _business.GetAll(pagedParams);
                
                if (!response.Succeeded)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var listError = new string[] { ex.Message };

                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError));
            }
        }

        /// <summary>
        /// Create new Testimony
        /// </summary>
        /// <remarks>To create a new testimonial indicate name and description, and optional the image</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Post([FromForm] NewTestimonyDto newEntity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _business.InsertTestimonial(newEntity);
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[] { e.Message };
                return StatusCode(500, new Response<NewTestimonyDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
        /// <summary>
        /// Delete specific testimony by id
        /// </summary>
        /// <param name="id">Numeric identifier from testimony</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _business.DeleteTestimonial(id);

                if (result.Succeeded == false)
                    return StatusCode(400, result);

                return Ok(result);
            }
            catch (Exception e)
            {
                var listErrors = new string[] { e.Message };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
        /// <summary>
        /// Update testimony
        /// </summary>
        /// <remarks>To update a testimonial indicate name and description, and optional the image</remarks>
        /// <param name="id">Numeric identifier from testimony</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put(int id, [FromForm] TestimonyInputDto testimonyInput)
        {
            try
            {
                var result = await _business.UpdateTestimonial(id, testimonyInput);
                return result.Succeeded == true ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(null, false, new string[] {e.Message}, "Server Error" ));
            }
        }
    }
}