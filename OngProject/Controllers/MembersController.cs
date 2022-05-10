using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;
using OngProject.Core.Models.Pagination;

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
        public async Task<IActionResult> GetAll([FromQuery] PagedListParams pagedParams)
        {
            try
            {
                var response = await _membersBusiness.GetMembers(pagedParams);
                if (!response.Succeeded)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var listError = new[] { e.Message };
                return StatusCode(500, new Response<string>(null, false, listError , "Error"));
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Insert(NewMemberDTO newMemberDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var response = await _membersBusiness.InsertMember(newMemberDTO);
                if (response.Succeeded == false)
                {
                    return BadRequest(response);               
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var listErrors = new string[] { e.Message };
                return StatusCode(500, new Response<string>(null, false, listErrors, "Error"));
            }

            
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _membersBusiness.DeleteMember(id);
                if (response.Succeeded == false)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                var listError = new string[] { e.Message };
                return StatusCode(500, new Response<string>(null, false, listError, "Error"));
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] NewMemberDTO memberUpdate)
        {
            try
            {
                var result = await _membersBusiness.UpdateMemberAsync(id, memberUpdate);
                return result.Succeeded == true ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(null, false, new string[] { e.Message }, "Server Error"));
            }
        }
    }
}
