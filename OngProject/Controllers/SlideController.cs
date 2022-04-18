using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Entities;
using System.Collections.Generic;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        [HttpGet]
        public IActionResult All()
        {
            try
            {
                return Ok(new List<Slide>());
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            try
            {
                return Ok(new Slide());
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] Slide slide)
        {
            try
            {
                return Ok("Succes");
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Slide slide)
        {
            try
            {
                return Ok("Succes");
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok("Succes");
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
