using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        
        public NewsBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = mapper;
        }
        
        public Task<IEnumerable<New>> GetNews(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<NewDto>> GetNew(int id)
        {
            var response = new Response<NewDto>();
            try
            {
                var entity = await _unitOfWork.NewRepository.GetById(id, "Comments");

                if (entity == null)
                {
                    response.Data = null;
                    response.Succeeded = false;
                    response.Message = "Datos incorrectos";
                }
                else
                {
                    response.Data = _entityMapper.NewToNewDto(entity);
                    response.Succeeded = true;
                    response.Message = "Correcto";
                }
            }
            catch (Exception e)
            {
                var listErrors = new string[] {e.Message, e.StackTrace};
                response.Errors = listErrors;
                response.Message = "ocurrio un error!!";
            }

            return response;
        }

        public Task<New> InsertNew(New entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateNew(int id, New entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteNew(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}