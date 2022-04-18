﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IUsersBusiness
    {
        Task<IEnumerable<User>> GetUsers(bool listEntity);
        Task<User> GetUser(int id);
        Task<User> InsertUser(User entity);
        Task UpdateUser(int id, User entity);
        Task DeleteUser(int id);
    }
}
