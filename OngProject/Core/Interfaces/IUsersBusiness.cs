using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IUsersBusiness
    {
        Task<IEnumerable<UserDto>> GetUsers(bool listEntity);
        Task<User> GetUser(int id);
        Task<UserDto> InsertUser(RegisterDto registerDto);
        Task UpdateUser(int id, User entity);
        Task DeleteUser(int id);
    }
}
