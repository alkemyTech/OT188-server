using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersBusiness _membersBusiness;

        public MembersController(IMembersBusiness membersBusiness)
        {
            _membersBusiness = membersBusiness;
        }


        [Authorize(Roles="Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listMembers = await _membersBusiness.GetMembers(true);
                return Ok(listMembers);
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(NewMemberDTO newMemberDTO)
        {
            {
                var response = await _membersBusiness.InsertMember(newMemberDTO);
                return response.Errors == null ? Ok(response) : StatusCode(500, response);
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
