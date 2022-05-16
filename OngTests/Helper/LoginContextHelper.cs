using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;

namespace OngTests.Helper
{
    public class LoginContextHelper
    {
        public static async Task<ControllerContext> GetLoginContext(string email, string password, IAuthBusiness authBusiness)
        {
            var loginDto = new LoginDto
            {
                Email = email,
                Password = password
            };

            var userBusiness = new UsersBusiness(
                            ContextHelper.UnitOfWork,
                            ContextHelper.EntityMapper,
                            ContextHelper.EmailServices,
                            ContextHelper.Config,
                            ContextHelper.JwtHelper,
                            ContextHelper.httpContext);
            var loginController = new AuthController(authBusiness, userBusiness); 

            var loginResponse =  loginController.Login(loginDto);
            var response = loginResponse as OkObjectResult;
            string token = response.Value.ToString();

            var userResult = await ContextHelper.UnitOfWork.UserRepository.FindByAsync(x => x.Email == email);
            var user = userResult.FirstOrDefault();

            var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                        new Claim(ClaimTypes.Name, user.Email),
                                        new Claim(ClaimTypes.Role, user.Roles?.Name)
                                   }, "TestAuthentication"));

            var httpContext = new DefaultHttpContext() { User = userClaim };
            httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

            return new ControllerContext()
            {
                HttpContext = httpContext,
            };
        }
    }
}
