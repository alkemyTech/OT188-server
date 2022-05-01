using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        Task<IEnumerable<New>> GetNews(bool listEntity);
        Task<New> GetNew(int id);
        Task<Response<NewDTO>> InsertNew(NewDTO entity);
        Task UpdateNew(int id, New entity);
        Task DeleteNew(int id);
    }
}