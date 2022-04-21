using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEntityMapper
    {
        public Task<OrganizationDTO> ConvertToOrganizationDTO(Organization organization);
    }
}
