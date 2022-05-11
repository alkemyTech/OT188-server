using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;

        public UsersController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
        }

        /// <summary>
        /// get user list.
        /// </summary>
        /// 
        /// <remarks>
        /// returns list of active users  or corresponding errors
        /// 
        /// Sample request:
        /// 
        ///     GET / USERS
        ///       "firstName": "nicolas",
        ///       "lastName": "alkemy",
        ///       "email": "email@gmail.com",
        ///       "photo": "image profile",
        ///       "rol": "administrador"
        /// 
        /// </remarks>
        /// <response code="200">Return the list of users with some details</response>
        /// <response code="500">If the server fails</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listUser = await _usersBusiness.GetUsers(true);
                return Ok(listUser);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        /// <summary>
        /// Delete user by id.
        /// </summary>
        
        ///<param name = "id">user id to delete</param>
        /// <response code="200">Succes, entity deleted successfully</response>
        /// <response code="403">user not found</response>
        /// <response code="401">unauthorised</response>
        /// <response code="500">If the server fails</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _usersBusiness.DeleteUser(id);

                return response.Succeeded == false ? StatusCode(403, response) : Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        ///  Update user 
        /// </summary>
        /// <remarks>
        /// update data of user by id user
        /// 
        /// Sample request:
        /// 
        ///     PUT/
        ///       "firstName": "nicolas" 
        ///       "lastName": "alkemy", 
        ///       "email": "email@gmail.com", 
        ///       "Password": "xxxxxxxx"     
        ///       "photo": "image profile",
        ///       
        /// </remarks>
        /// 
        /// <param name="id">user id to update.*required</param>
        /// <param name="userUpdate"></param>
        /// <response code="200">Succes, entity updated with data</response>
        /// <response code="404">not found</response>
        /// <response code="401">unauthorised</response>
        /// <response code="500">If the server fails</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromForm] RegisterDto userUpdate)
        {
            try
            {
                var result = await _usersBusiness.UpdateUserAsync(id, userUpdate);
                return result.Succeeded == true ? Ok(result) : NotFound(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new Response<string>(null, false, new string[] { e.Message }, "Server Error"));
            }
        }
    }
}
