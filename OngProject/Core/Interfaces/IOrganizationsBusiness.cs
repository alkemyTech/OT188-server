using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsBusiness
    {
        
        Task<IEnumerable<Organizations>> GetOrganizations(bool listEntity);
        Task<Organizations> GetOrganization(int id);
        Task<Organizations> InsertOrganization(Organizations entity);
        Task UpdateOrganization(int id, Organizations entity);
        Task DeleteOrganization(int id);
        
    }
    
}