using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
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
        public AuthBusiness(IUnitOfWork unitOfWork, IJwtTokenProvider jwtTokenProvider,IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenProvider = jwtTokenProvider;
            _entityMapper = entityMapper;
        }
        public AuthUserDto LoginUser(LoginDto login)
        {
            var user = _unitOfWork.RepositoryAuth.GetUserByEmailOrDefault(login);
            if (user !=null)
            {
                if (EncryptSha256.SamePasswords(user.Result.Password,login.Password))
                {
                    string token = _jwtTokenProvider.CreateJwtToken(user.Result).Result;

                    return _entityMapper.UserToAuthUserDto(user.Result,token);
                }
                throw new Exception("Email o Contraseña incorrecta.");
            }
            throw new Exception("El email ingresado no existe."); 
        }
    }
}
