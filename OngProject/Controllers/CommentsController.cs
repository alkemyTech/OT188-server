using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{


    [Route("comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsBusiness _commentsBusiness;
        public CommentsController(ICommentsBusiness commentsBusiness)
        {
            _commentsBusiness = commentsBusiness;
        }
        [HttpDelete("{Id}"), Authorize]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = await _commentsBusiness.DeleteComments(Id);
                if (result.Succeeded == false)
                {
                    return StatusCode(403, result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new Response<string>()
                {
                    Data = "Error - 404",
                    Message = ex.Message,
                    Succeeded = false
                };
                return StatusCode(404, response);
            }
        }
            [HttpPost]
            public async Task<IActionResult> Insert(NewCommentDto newCommentDto)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest();
                    }
                    var response = await _commentsBusiness.InsertComment(newCommentDto);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    var listError = new string[] { ex.Message };
                    return StatusCode(500, new Response<NewCommentDto>(data: null, succeeded: false, errors: listError, message: "Error"));

                }


            }

    }





    
}
