using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface IOrganizationsBusiness
    {

        Task<Response<IEnumerable<OrganizationDTO>>> GetOrganizations(bool listEntity);
        Task<Response<OrganizationDTO>> GetOrganization(int id);
        Task<Response<UpdateOrganizationDTO>> UpdateOrganizations(UpdateOrganizationDTO entity);
    }
    
}