using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class RolesBusiness : IRolesBusiness
    {

        private readonly IUnitOfWork _unitOfWork;

        public RolesBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<Roles>> GetRoles(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Roles> GetRol(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Roles> InsertRol(Roles entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateRol(int id, Roles entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteRol(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}