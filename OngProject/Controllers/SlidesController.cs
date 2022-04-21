using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {

        private readonly ISlidesBusiness _slidesBusiness;
        public SlidesController(ISlidesBusiness slidesBusiness)
        {
            _slidesBusiness = slidesBusiness;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var listSlides = await _slidesBusiness.GetSlides(true);
                return Ok(listSlides);
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
