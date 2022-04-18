using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class OrganizationsBusiness : IOrganizationsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public OrganizationsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Task<IEnumerable<Organization>> GetOrganizations(bool listEntity)
        {
            throw new NotImplementedException();
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