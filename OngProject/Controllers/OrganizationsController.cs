using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _business;

        public OrganizationsController(IOrganizationsBusiness business)
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
        public async Task<ActionResult> Post([FromForm] Organization entity)
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