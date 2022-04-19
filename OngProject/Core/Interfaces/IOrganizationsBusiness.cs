using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsBusiness
    {
        
        Task<IEnumerable<Organization>> GetOrganizations(bool listEntity);
        Task<Organization> GetOrganization(int id);
        Task<Organization> InsertOrganization(Organization entity);
        Task UpdateOrganization(int id, Organization entity);
        Task DeleteOrganization(int id);
        
    }
    
}