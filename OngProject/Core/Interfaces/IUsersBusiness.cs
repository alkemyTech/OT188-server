using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IUsersBusiness
    {
        Task<IEnumerable<UserDto>> GetUsers(bool listEntity);
        Task<User> GetUser(int id);
        Task<Response<string>> InsertUser(RegisterDto registerDto);
        Task<Response<UserOutDTO>> UpdateUserAsync(int id, RegisterDto update);
        Task<Response<string>> DeleteUser(int id);
        Task<Response<UserOutDTO>> GetMe();
    }
}
