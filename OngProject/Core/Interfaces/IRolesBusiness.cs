using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IRolesBusiness
    {
        Task<IEnumerable<Roles>> GetRoles(bool listEntity);
        Task<Roles> GetRol(int id);
        Task<Roles> InsertRol(Roles entity);
        Task UpdateRol(int id, Roles entity);
        Task DeleteRol(int id);
    }
}