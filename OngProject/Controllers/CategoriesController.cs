using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBusiness _business;

        public CategoriesController(ICategoriesBusiness business)
        {
            _business = business;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllNames([FromQuery] int pagedParams)
        {
            try
            {
                var response = await _business.GetNameList(pagedParams);
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
        /// Get category by id 
        /// </summary>
        /// <param name="id">is numeric identifier from category</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromForm] int id)
        {
            try
            {
                var result = await _business.GetCategory(id);
                if (result.Succeeded == false)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var listError = new[]
                {
                    ex.Message
                };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError));
            }
        }
        /// <summary>
        /// Create new Category
        /// </summary>
        /// <remarks>To create a new category indicate name, is required, description and image as optional</remarks>
        /// Sample request:
        /// 
        ///    POST
        ///    {
        ///       "name": "string",
        ///       "description": "string",
        ///       "image": "string"
        ///    }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromForm] NewCategoryDTO categoriesNewsDTO)
        {
            try
            {
              
                   if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    var response = await _business.InsertCategory(categoriesNewsDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Update a Category values
        /// </summary>
        /// <remarks>To update a category indicate name, description and image</remarks>
        /// <param name="id"> is numeric identifier from category</param>
        /// Sample request:
        /// 
        ///    PUT
        ///    {
        ///       "name": "string",
        ///       "description": "string",
        ///       "image": "string"
        ///    }
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromForm] NewCategoryDTO categoryDto, int id)
        {
            try
            {
                if (!ModelState.IsValid) 
                    BadRequest();

                var response = await _business.UpdateCategory(id, categoryDto);
      
                return Ok(response);
            }
            catch (Exception e)
            {
                var listError = new[]
                {
                    e.Message
                };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError,"Error"));
            }
        }

        /// <summary>
        /// Delete specific category by id
        /// </summary>
        /// <param name="id"> is numeric identifier from category</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _business.DeleteCategory(id);

                if (result.Succeeded == false)
                    return StatusCode(403, result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}