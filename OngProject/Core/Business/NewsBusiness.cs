using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
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

        public NewsBusiness(IUnitOfWork unitOfWork,IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }
        
        public Task<IEnumerable<New>> GetNews(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<New> GetNew(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<NewDTO>> InsertNew(NewDTO entity)
        {
            var response = new Response<NewDTO>();

            if ((entity.Name is String) == true)
            {
                try
                 {               
                    var newEntity = _entityMapper.NewDTOToNew(entity);                    
                    newEntity.Comments = null;
                    newEntity.IsDeleted = false;
                    newEntity.ModifiedAt= DateTime.UtcNow;
                    
                    var newEntityResult = await _unitOfWork.NewRepository.AddAsync(newEntity);
                    await _unitOfWork.SaveChangesAsync();
                    response.Data = entity;
                    response.Succeeded = true;
                    response.Message = "New creada correctamente";               
                }
                catch (Exception e)
                    {
                        var listErrors = new string[] { e.Message, e.StackTrace };
                        response.Errors = listErrors;
                    }
            }
            else
            {
                response.Succeeded = false;
                response.Message = "Datos incorrectos";
            }
            return response;           
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