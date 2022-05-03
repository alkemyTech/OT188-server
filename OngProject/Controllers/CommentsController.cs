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
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);

            }


        }





    }
}
