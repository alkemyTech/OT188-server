using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("organization")]
    [ApiController]
    
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _business;
        private readonly IEntityMapper _entityMapper;

        public OrganizationsController(IOrganizationsBusiness business, IEntityMapper entityMapper)
        {
            _business = business;
            _entityMapper = entityMapper;
        }

        [HttpGet]
        [Route("public")]
        public async Task<IActionResult> GetAll(bool listEntity = false)
        {
            try
            {
                var organizationList = await _business.GetOrganizations(listEntity);
                var result = _entityMapper.ConvertToOrganizationDTO(organizationList.SingleOrDefault());            

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
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