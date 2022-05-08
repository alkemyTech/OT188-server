using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
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
                var result = await _slidesBusiness.GetSlides(true);
                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var result = await _slidesBusiness.GetDetailSlide(id);
                if(result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddSlideDTO add)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _slidesBusiness.Add(add);
                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] UpdateSlideDTO data, int id)
        {
            try
            {
                if (data.Image == null &&
                    data.Order == null &&
                    data.OrganizationId == null &&
                    data.Text == null)
                {
                    return BadRequest("Indique por lo menos un campo a modificar");
                }
                var result = await _slidesBusiness.Update(data, id);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                var error = new Response<string>(e.Message, false, null, "Server Error");
                return StatusCode(500, error);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _slidesBusiness.Delete(id);

                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, null, "Error"));
            }
        }
    }
}
