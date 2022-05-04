using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
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

                return response.Succeeded == false ? StatusCode(403, response) : Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] NewDTO entity)
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
                return StatusCode(500, ex);
            }
        }
    }
}