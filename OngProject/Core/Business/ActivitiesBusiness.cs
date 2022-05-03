using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ActivitiesBusiness : IActivitiesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;

        public ActivitiesBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }

        public async Task<Response<NewActivityDto>> InsertActivity(NewActivityDto entity)
        {
            var result = new Response<NewActivityDto>();
            try
            {                
                var activity = _entityMapper.ActivityDtoToActivity(entity);
                await _unitOfWork.ActivityRepository.AddAsync(activity);
                await _unitOfWork.SaveChangesAsync();
                result.Data = entity;
                result.Succeeded = true;
                result.Message = $"The activity has been created";
            }
            catch (Exception e)
            {                 
                throw;
            }
            return result;
        }
    }
}
