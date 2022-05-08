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

        public async Task<Response<ActivityOutDTO>> InsertActivity(NewActivityDto entity)
        {
            var result = new Response<ActivityOutDTO>();
            try
            {
                var activity = _entityMapper.ActivityDtoToActivity(entity);
                await _unitOfWork.ActivityRepository.AddAsync(activity);
                await _unitOfWork.SaveChangesAsync();
                var activityOut = _entityMapper.ActivityToActivityOutDto(activity);
                result.Data = activityOut;
                result.Succeeded = true;
                result.Message = $"The activity has been created";
            }
            catch (Exception e)
            {                 
                throw ;
            }
            return result;
        }

        public async Task<Response<string>> UpdateActivity(NewActivityDto entity, int id)
        {
            var activity = await _unitOfWork.ActivityRepository.GetById(id);
            if (activity == null || activity.IsDeleted == true)
            {
                return new Response<string>("Not Found", false, null, "Error");
            }
            var updateActivity = _entityMapper.ActivityDtoToActivity(entity);
            activity = _entityMapper.UpdateActivity(activity, updateActivity);
            await _unitOfWork.ActivityRepository.Update(activity);
            _unitOfWork.SaveChanges();

            return new Response<string>("Entidad Actualizada");
        }
    }
}
