using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/comments")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

    public class CommentsController : ControllerBase
    {
        private readonly ICommentsBusiness _CommentsBusiness;

        public CommentsController(ICommentsBusiness CommentsBusiness)
        {
            _CommentsBusiness = CommentsBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var CommentsList = await _CommentsBusiness.GetComments(true);

                if (CommentsList == null)
                {
                    return NotFound();
                }

                return Ok(CommentsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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