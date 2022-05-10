using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsBusiness _business;

        public NewsController(INewsBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
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

        [HttpGet("{id}"), Authorize(Roles = "Administrator")]
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

        [HttpGet("{id}/comments")]
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

        [HttpPost]
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

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
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