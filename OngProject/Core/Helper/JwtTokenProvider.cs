using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class JwtTokenProvider : IJwtTokenProvider
    {

        private readonly UserManager<User> _userManager;

        public JwtTokenProvider(IConfiguration configuration , UserManager<User> usermanager)
        {
            Configuration = configuration;
            _userManager = usermanager;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> CreateJwtToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, (string)user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    Configuration.GetSection("AppSettings:DevelopmentApiKey").Value));

            var tPayLoad = new JwtSecurityToken(

                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: authClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tPayLoad); 


            return token;
        }
    }
}
