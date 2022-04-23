using System;
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
        
        public Task<IEnumerable<Rol>> GetRoles(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Rol> GetRol(int id)
        {
            var rol = _unitOfWork.RolRepository.GetById(id);
            if (rol.Result != null)
            {
                return rol;
            }
            throw new Exception("Rol null");
        }

        public Task<Rol> InsertRol(Rol entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateRol(int id, Rol entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteRol(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}