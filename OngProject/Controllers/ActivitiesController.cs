using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Insert([FromForm] NewActivityDto activityDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _activitiesService.InsertActivity(activityDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[]{ e.Message};
                return StatusCode(500, new Response<NewActivityDto>(data: null, succeeded: false, errors: listErrors, message: "Server Error"));
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Update([FromForm] UpdateActivityDTO data, int id)
        {
            try
            {
                var result = await _activitiesService.UpdateActivity(data, id);
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new Response<string>(e.Message, false, null, "Server Internal Error");
                return StatusCode(500, error);
            }
        }
    }
}
