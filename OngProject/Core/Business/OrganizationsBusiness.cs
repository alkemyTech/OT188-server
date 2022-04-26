using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class OrganizationsBusiness : IOrganizationsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        
        public OrganizationsBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }


        public async Task<IEnumerable<OrganizationDTO>> GetOrganizations(bool listEntity)
        {
            var organizationsList = await _unitOfWork.OrganizationsRepository.GetAll(org => org.IsDeleted == false, org => org.Slides.OrderBy(sl => sl.Order));

            if (organizationsList == null)
            {
                return null;
            }
            var organizationDTOList = new List<OrganizationDTO>();
            organizationDTOList.Add(_entityMapper.OrganizationToOrganizationDTO(organizationsList.SingleOrDefault()));

            return organizationDTOList;
        }

        public Task<Organization> GetOrganization(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Organization> InsertOrganization(Organization entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrganization(int id, Organization entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrganization(int id)
        {
            throw new NotImplementedException();
        }
    }
}