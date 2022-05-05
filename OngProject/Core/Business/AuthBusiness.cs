using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IEntityMapper _entityMapper;
        public AuthBusiness(IUnitOfWork unitOfWork, IJwtTokenProvider jwtTokenProvider, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenProvider = jwtTokenProvider;
            _entityMapper = entityMapper;
        }
        public Response<AuthUserDto> LoginUser(LoginDto login)
        {
            var response = new Response<AuthUserDto>();
            try
            {
                var user = _unitOfWork.RepositoryAuth.GetUserByEmailOrDefault(login);
                if (user.Result != null)
                {
                    if (EncryptSha256.SamePasswords(user.Result.Password, login.Password))
                    {
                        string token = _jwtTokenProvider.CreateJwtToken(user.Result).Result;

                        response.Data = _entityMapper.UserToAuthUserDto(user.Result, token);
                        response.Succeeded = true;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.Succeeded = false;
                        response.Message = "Email o Contraseña incorrecta.";
                    }
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "El email ingresado no existe.";
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }     
    }
}