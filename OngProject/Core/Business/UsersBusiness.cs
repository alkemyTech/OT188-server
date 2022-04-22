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
    public class UsersBusiness : IUsersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        
        public UsersBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<UserDto>> GetUsers(bool listEntity)
        {
            try
            {
                var listUser =  await _unitOfWork.UserRepository.GetAll(listEntity, "Roles");

                return listUser.Select(user => _mapper.UserToUserDto(user)).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> InsertUser(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(int id, User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}
