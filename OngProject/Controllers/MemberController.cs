using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMembersService _membersService;

        public MemberController(IMembersService membersService)
        {
            _membersService = membersService;
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
        public async Task<IActionResult> Insert()
        {
            return Ok();
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
