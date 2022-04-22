using Microsoft.EntityFrameworkCore;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class RepositoryAuth :  Repository<User>, IRepositoryAuth
    {
        public RepositoryAuth(OngProjectDbContext context):base(context)
        {
        }
        public async Task<User> GetUserByEmailOrDefault(LoginDto login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email==login.Email && x.IsDeleted == false);
        }
    }
}
