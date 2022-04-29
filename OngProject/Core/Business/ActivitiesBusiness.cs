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

        public async Task<Response<ActivityDto>> InsertActivity(ActivityDto entity)
        {
            if (entity == null)
            {
                var result = new Response<ActivityDto>(entity, false, message: "Error");
                result.Errors[0] = "Status Code: 400 Bad Request";
                return result;
            }
            var activity = _entityMapper.ActivityDtoToActivity(entity);
            activity.DeletedAt = new DateTime();//DateTime vacío.

            await _unitOfWork.ActivityRepository.AddAsync(activity);
            await _unitOfWork.SaveChangesAsync();

            return new Response<ActivityDto>(entity, message: $"The \"{entity.Name}\" activity has been created");
        }
    }
}
