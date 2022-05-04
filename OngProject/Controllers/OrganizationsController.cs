using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("organization")]
    [ApiController]
    
    public class OrganizationsController : ControllerBase
    {
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
                var result = await _organizationsBusiness.GetOrganizations(true);
                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, message: "Server Error"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDTO>> Get(int id)
        {
            try
            {                
                var result = await _organizationsBusiness.GetOrganization(id);

                if (result.Succeeded == false)
                {
                    return StatusCode(400, result);
                }
                return Ok(result);
            }           
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(e.Message, false, message: "Server Error"));
            }
        }
    }
}