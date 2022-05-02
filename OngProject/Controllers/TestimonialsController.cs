using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsBusiness _business;

        public TestimonialsController(ITestimonialsBusiness business)
        {
            _business = business;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return NoContent();
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }

        [HttpPost]
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
                var listErrors = new string[2];
                listErrors[0] = e.Message;
                listErrors[1] = e.StackTrace;
                return StatusCode(500, new Response<NewTestimonyDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] Organization entity, int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }
    }
}