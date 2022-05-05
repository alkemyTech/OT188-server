using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

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
                return StatusCode(500, new Response<NewActivityDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
    }
}