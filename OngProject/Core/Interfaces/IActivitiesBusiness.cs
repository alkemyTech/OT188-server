using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivitiesBusiness
    {
        Task<Response<ActivityOutDTO>> InsertActivity(NewActivityDto entity);
        Task<Response<ActivityOutDTO>> UpdateActivity(UpdateActivityDTO entity, int id);
    }
}
