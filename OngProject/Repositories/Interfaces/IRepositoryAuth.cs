using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepositoryAuth : IRepository<User>
    {
        Task<User> GetUserByEmailOrDefault(LoginDto login);
    }
}
