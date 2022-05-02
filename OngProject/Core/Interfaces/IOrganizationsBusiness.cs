using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsBusiness
    {
        
        Task<IEnumerable<OrganizationDTO>> GetOrganizations(bool listEntity);
        Task<OrganizationDTO> GetOrganization(int id);
        Task<Organization> InsertOrganization(Organization entity);
        Task UpdateOrganization(int id, Organization entity);
        Task DeleteOrganization(int id);
        
    }
    
}