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


        public Task<IEnumerable<Organizations>> GetOrganizations(bool listEntity)
        {
            throw new NotImplementedException();
        }

        public Task<Organizations> GetOrganization(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Organizations> InsertOrganization(Organizations entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrganization(int id, Organizations entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrganization(int id)
        {
            throw new NotImplementedException();
        }
    }
}