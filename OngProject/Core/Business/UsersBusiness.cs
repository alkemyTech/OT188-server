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
using OngProject.Core.Models;
using Swashbuckle.Swagger;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OngProject.Core.Business
{
    public class UsersBusiness : IUsersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IEmailServices _emailService;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenProvider _jwtToken;
        private readonly IHttpContextAccessor _accessor;

        public UsersBusiness(IUnitOfWork unitOfWork,
                            IEntityMapper mapper, 
                            IEmailServices emailService, 
                            IConfiguration configuration, 
                            IJwtTokenProvider jwtToken,
                            IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _configuration = configuration;            
            _jwtToken = jwtToken;
            _accessor = accessor;
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

        public async Task<Response<string>> InsertUser(RegisterDto registerDto)
        {
            var response = new Response<string>();
            try
            {
                var ListUsers = _unitOfWork.UserRepository.GetAll(true).Result;
                if (ListUsers != null)
                {
                    if (!Exist(ListUsers, registerDto.Email))
                    {
                        var registeredUser = _mapper.RegisterDtoToUser(registerDto);

                        registeredUser.Password = EncryptSha256.Encrypt(registeredUser.Password);

                        registeredUser.RolesId = RoleTypes.Standard;

                        try
                        {
                            var entity = await _unitOfWork.UserRepository.AddAsync(registeredUser);

                            await _unitOfWork.SaveChangesAsync();

                            var user = await _unitOfWork.UserRepository.GetById(entity.Id, "Roles");

                            var subject = "Confirmación de registro";

                            var body = $"Bienvenido {user.FirstName} {user.LastName}";

                            await _emailService.Send(user.Email, _configuration.GetSection("emailContacto").Value, subject, body);

                            var token = _jwtToken.CreateJwtToken(user);
                            //await Task.FromResult(token.Result);
                            response.Data = token.Result;
                            response.Message = "token return";
                            response.Succeeded = true;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        response.Message = "Ya existe un usuario con ese email.";
                        response.Succeeded = false;
                    }
                }
                else
                {
                    response.Message = "El campo email no puede quedar vacio.";
                    response.Succeeded = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return response;
        }
               
        public async Task<Response<UserOutDTO>> UpdateUserAsync(int id, RegisterDto update)
        {
            try
            {
                var userId = await _unitOfWork.UserRepository.GetById(id);
                if (userId == null)
                    return new Response<UserOutDTO>(null, false, null, "Entity Not Found");

                var user = _mapper.RegisterDtoToUser(update,userId);

                await _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
                var userResponse = _mapper.UserToUserOutDTO(user);
                return new Response<UserOutDTO>(userResponse, true, null, "Success!");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Response<string>> DeleteUser(int id)
        {
            try
            {
                await _unitOfWork.UserRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                return new Response<string>("Error", succeeded: false, message: e.Message);
            }
            await _unitOfWork.SaveChangesAsync();
            return new Response<string>("Succes", message: "Entity Deleted");
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

        public async Task<Response<UserOutDTO>> GetMe()
        {

            if (_accessor.HttpContext != null)
            {
                var id = int.Parse(_accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var entity = await _unitOfWork.UserRepository.GetById(id);

                var entityDto = _mapper.UserToUserOutDTO(entity);

                return new Response<UserOutDTO>(entityDto);
            }

            return null;
        }
    }
}
