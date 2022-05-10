using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;

namespace OngProject.Controllers
{
    /// <summary>
    /// News controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _business;

        public NewsController(INewsBusiness business)
        {
            _business = business;
        }
        
        /// <summary>
        /// Get all news (paginated)
        /// </summary>
        /// <param name="pagedParams"></param>
        /// <remarks>Indicate the number and size of page</remarks>
        /// <response code="200">Ok.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(PagedListResponse<NewDTO>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PagedListResponse<NewDTO>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll([FromQuery] PagedListParams pagedParams)
        {
            try 
            {
                var response = await _business.GetAll(pagedParams);
                if (!response.Succeeded  )
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                var listError = new string[] { ex.Message };

                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError));
            }          
        }

        /// <summary>
        /// Get by id the new
        /// </summary>
        /// <param name="id">Numeric identifier from new</param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<NewOutDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewOutDto>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var response = await _business.GetNew(id);

                return response.Succeeded == false ? BadRequest(response) : Ok(response);
            }
            catch (Exception ex)
            {
                var listError = new string[]{ ex.Message };

                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError));
            }
        }
        
        /// <summary>
        /// Get by id the comment
        /// </summary>
        /// <param name="id">Numeric identifier from comment</param>
        /// <returns></returns>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<NewWithCommentsDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<NewWithCommentsDto>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNewComments(int id) 
        {
            try
            {
                var response = await _business.GetNewComments(id);

                if (!response.Succeeded)
                {
                    return NotFound(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var listErrors = new[] { ex.Message };

                return StatusCode(500, new Response<NewWithCommentsDto>(null, false, listErrors, "Internal Server Error"));
            }
        }


        /// <summary>
        /// Create new News
        /// </summary>
        /// <param name="entity"></param>
        /// <remarks><remarks>To create a new new indicate name and content, and optional the image, and category id</remarks></remarks>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<CreateNewOutDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CreateNewOutDto>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromForm] CreateNewDto entity)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    return BadRequest();
                }

                var response = await _business.InsertNew(entity);
                
                if (response.Succeeded == false)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }            
            catch (Exception ex)
            {
                var listErrors = new string[] { ex.Message };
                return StatusCode(500, new Response<CreateNewOutDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
        
        /// <summary>
        /// Delete specific new by id
        /// </summary>
        /// <param name="id">Numeric identifier from new</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _business.DeleteNew(id);
                return result.Succeeded == true ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(null, false, new string[] { e.Message }, "Error! Entity not found"));
            }
        }
        
        /// <summary>
        /// Update new
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto">Numeric identifier from new</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<UpdateNewOutDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<UpdateNewOutDto>),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateNewDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var response = await _business.UpdateNew(id, dto);

                if (!response.Succeeded)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var listErrors = new string[] { ex.Message };
                return StatusCode(500, new Response<UpdateNewOutDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
    }
}