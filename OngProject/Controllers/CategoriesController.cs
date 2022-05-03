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
    
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesBusiness _business;

        public CategoriesController(ICategoriesBusiness business)
        {
            _business = business;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllNames()
        {
            try
            {
                return Ok(await _business.GetNameList());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
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
                var listError = new string[]
                {
                    ex.Message
                };
                return StatusCode(500, new Response<string>(data: null, succeeded: false, errors: listError));
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm]NewCategoryDTO categoriesNewsDTO)
        {
            try
            {
                var response = await _business.InsertCategory(categoriesNewsDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _business.DeleteCategory(id);

                if (result.Succeeded == false)
                    return StatusCode(403, result);

                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}