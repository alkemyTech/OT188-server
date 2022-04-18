using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;

        public UsersController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
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
        public async Task<ActionResult> Post([FromForm] User entity)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] User entity, int id)
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
