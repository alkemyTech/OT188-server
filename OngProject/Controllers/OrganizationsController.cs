using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("organization")]
    [ApiController]
    
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _business;
        private readonly IOrganizationsBusiness _organizationsBusiness;

        public OrganizationsController(IOrganizationsBusiness organizationsBusiness)
        {
            _organizationsBusiness = organizationsBusiness;
        }

        [HttpGet]
        [Route("public")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _business.GetOrganizations(true);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDTO>> Get(int id)
        {
            try
            {                
                var organization = await _organizationsBusiness.GetOrganization(id);

                if (organization == null)
                {
                    return NotFound(organization);
                }

                return Ok(organization);
            }           
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}