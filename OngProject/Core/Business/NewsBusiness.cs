using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public NewsBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }

        public Task<IEnumerable<New>> GetNews(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<NewOutDto>> GetNew(int id)
        {
            var response = new Response<NewOutDto>();
            try
            {
                var entity = await _unitOfWork.NewRepository.GetById(id, "Comments");

                if (entity != null && entity.IsDeleted != true)
                {
                    response.Data = _entityMapper.NewToNewOUtDto(entity);
                    response.Succeeded = true;
                    response.Message = "Success.";
                }
                else
                {
                    response.Succeeded = false;
                    response.Message = "New is deleted or not exist.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<Response<NewWithCommentsDto>> GetNewComments(int id)
        {
            var response = new Response<NewWithCommentsDto>();
            try
            {
                var entity = await _unitOfWork.NewRepository.GetById(id, "Comments");

                if (entity != null)
                {
                    var newWithCommentsDto = _entityMapper.NewToNewWithCommentsDto(entity);

                    response.Data = newWithCommentsDto;

                    response.Succeeded = true;
                }
                else
                {
                    response.Data = null;

                    response.Succeeded = false;

                    response.Message = "Entity not found";
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }

        public async Task<Response<CreateNewOutDto>> InsertNew(CreateNewDto dto)
        {
            
            var response = new Response<CreateNewOutDto>();            

            try
            {
                var nameCollection = await _unitOfWork.NewRepository.FindByAsync(n => n.Name == dto.Name);
                var categoryCollection = await _unitOfWork.NewRepository.FindByAsync(n => n.CategoryId == dto.CategoryId);

                if (nameCollection.Count == 0)
                {
                    if (categoryCollection.Count == 0)
                    {
                        response.Succeeded = false;
                        response.Message = "No existe una categoria con ese ID.";
                    }
                    else
                    {
                        var newEntity = _entityMapper.CreateNewDtoToNew(dto);

                        newEntity.ModifiedAt = DateTime.Now;

                        var newEntityResult = await _unitOfWork.NewRepository.AddAsync(newEntity);

                        await _unitOfWork.SaveChangesAsync();

                        var entityDto = _entityMapper.NewToCreateNewOutDto(newEntity);

                        response.Data = entityDto;

                        response.Succeeded = true;

                        response.Message = "Novedad creada correctamente.";
                    }                    
                }
                else
                {
                    response.Succeeded = false;

                    response.Message = "Ya existe una novedad con nombre similar en nuestros registros.";
                }                              
            }
            catch (Exception e)
            {
                //var listErrors = new string[] { e.Message };
                //return new Response<CreateNewOutDto>
                //{
                //    Data = null,
                //    Message = "Error",
                //    Succeeded = false,
                //    Errors = listErrors
                //};
                throw;
            }           
            
            return response;           
        }

        public Task UpdateNew(int id, New entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<string>> DeleteNew(int id)
        {
            var response = new Response<string>();
            try
            {
                await _unitOfWork.NewRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                response.Data = null;
                response.Errors = null;
                response.Succeeded = true;
                response.Message = "Success! Entity deleted";
                return response;
            }
            catch (InvalidOperationException e)
            {
                response.Data = null;
                response.Errors = new string[] { e.Message };
                response.Succeeded = false;
                response.Message = "Error! Entity not found";
                return response;
            }
        }
    }
}