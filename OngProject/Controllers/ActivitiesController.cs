using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("activities")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesBusiness _activitiesService;

        public ActivitiesController(IActivitiesBusiness activitiesService)
        {
            _activitiesService = activitiesService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(NewActivityDto activityDto)
        {
            try
            {
                var response = await _activitiesService.InsertActivity(activityDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[2] { e.Message, e.StackTrace };
                return StatusCode(500, new Response<NewActivityDto> { Data = null, Message = "Error", Succeeded = false, Errors = listErrors });
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }

        [Route("{id}")]
        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            return NoContent();
        }
    }
}
