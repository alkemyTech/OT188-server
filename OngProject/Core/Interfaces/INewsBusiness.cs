using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        Task<IEnumerable<New>> GetNews(bool listEntity);
        Task<New> GetNew(int id);
        Task<New> InsertNew(New entity);
        Task UpdateNew(int id, New entity);
        Task DeleteNew(int id);
    }
}