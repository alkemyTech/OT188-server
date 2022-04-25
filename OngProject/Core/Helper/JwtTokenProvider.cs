using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Interfaces;
using OngProject.Entities;
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

        private readonly IRolesBusiness _rolesBusiness;

        public JwtTokenProvider(IConfiguration configuration , IRolesBusiness rolesBusiness)
        {
            Configuration = configuration;
            _rolesBusiness = rolesBusiness;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> CreateJwtToken(User user)
        {
            var userRoles = await _rolesBusiness.GetRol(user.RolesId);
            
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRoles.Name) 
            };

            var authSigningKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    Configuration.GetSection("AppSettings:DevelopmentJwtApiKey").Value));

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
