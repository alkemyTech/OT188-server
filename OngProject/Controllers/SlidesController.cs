using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var detailSlide = await _slidesBusiness.GetDetailSlide(id);
                if(detailSlide == null)
                {
                    return NotFound();
                }
                return Ok(detailSlide);
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
