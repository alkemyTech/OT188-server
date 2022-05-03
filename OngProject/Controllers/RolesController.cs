using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesBusiness _rolesBusiness;

        public RolesController(IRolesBusiness rolesBusiness)
        {
            _rolesBusiness = rolesBusiness;
        }
    }
}