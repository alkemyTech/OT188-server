using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using OngProject.Core.Helper;
using OngProject.Core.Models.Enums;
using OngProject.Repositories;
using Microsoft.Extensions.Configuration;

namespace OngProject.Core.Business
{
    public class UsersBusiness : IUsersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IEmailServices _emailService;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenProvider _jwtToken;

        public UsersBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper, IEmailServices emailService, IConfiguration configuration, IJwtTokenProvider jwtToken)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _configuration = configuration;            
            _jwtToken = jwtToken;
        }


        public async Task<IEnumerable<UserDto>> GetUsers(bool listEntity)
        {
            try
            {
                if (!listEntity)
                {
                    var listUserAll = await _unitOfWork.UserRepository.GetAll(null);
                    
                    return listUserAll.Select(user => _mapper.UserToUserDto(user)).ToList();
                }
                
                var listUserFilter = await _unitOfWork.UserRepository.GetAll(x => x.IsDeleted == false, x => x.Roles);
                
                return listUserFilter.Select(user => _mapper.UserToUserDto(user)).ToList();
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

        public async Task<string> InsertUser(RegisterDto registerDto)
        {
            var ListUsers = _unitOfWork.UserRepository.GetAll(true).Result;
            if(ListUsers != null)
            {
                if (!Exist(ListUsers, registerDto.Email))
                {
                    var registeredUser = _mapper.RegisterDtoToUser(registerDto);

                    registeredUser.Password = EncryptSha256.Encrypt(registeredUser.Password);

                    registeredUser.RolesId = RoleTypes.Regular;
                    
                    try
                    {
                        var entity = await _unitOfWork.UserRepository.AddAsync(registeredUser);

                        await _unitOfWork.SaveChangesAsync();

                        var user = await _unitOfWork.UserRepository.GetById(entity.Id, "Roles");

                        var subject = "Confirmación de registro";

                        var body = $"Bienvenido {user.FirstName} {user.LastName}";
                        
                        await _emailService.Send(user.Email, _configuration.GetSection("emailContacto").Value, subject, body);
                        
                        var token = _jwtToken.CreateJwtToken(user);
                        
                        return await Task.FromResult(token.Result);
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                }

                throw new Exception("Ya existe un usuario con ese email.");
            }

            throw new Exception("El campo email no puede quedar vacio.");
        }
               
        public Task UpdateUser(int id, User entity)
        {
            throw new NotImplementedException();
        }
        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
        public bool Exist(IEnumerable<User> users, string email)
        {
            var exist = users.Where(user => user.Email == email).FirstOrDefault();
            if (exist != null)
            {
                return true;
            }

            return false;
        }
    }
}
