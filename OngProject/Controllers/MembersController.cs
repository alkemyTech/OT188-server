using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
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
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
                
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
                return StatusCode(500, e.Message);
            }
        }
    }
}
