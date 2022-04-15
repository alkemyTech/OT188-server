using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll(bool listEntity);

        Task<T> GetById(int id);
        
        Task<T> GetById(int id, string include);
        
        Task<T> Add(T entity);
        
        Task Update(T entity);
        
        Task Delete(int id);
    }
}
