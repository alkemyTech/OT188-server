using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IAuthBusiness
    {
        Response <AuthUserDto> LoginUser(LoginDto login);
    }
}