using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IRolesBusiness
    {
        Task<IEnumerable<Rol>> GetRoles(bool listEntity);
        Task<Rol> GetRol(int id);
        Task<Rol> InsertRol(Rol entity);
        Task UpdateRol(int id, Rol entity);
        Task DeleteRol(int id);
    }
}