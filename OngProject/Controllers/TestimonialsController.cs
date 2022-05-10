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
    [Route("testimonials")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsBusiness _business;

        public TestimonialsController(ITestimonialsBusiness business)
        {
            _business = business;
        }
       
        [HttpPost]
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

        [HttpDelete("{id}")]
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
        [HttpPut("{id}")]
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
            throw new NotImplementedException();
        }
    }
}