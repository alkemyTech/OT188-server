using System;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Post([FromForm] NewDTO entity)
        {
            try
            {              
                var response = await _business.InsertNew(entity);
                return Ok(response);
            }            
            catch (Exception ex)
            {
                return NoContent();
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] New entity, int id)
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