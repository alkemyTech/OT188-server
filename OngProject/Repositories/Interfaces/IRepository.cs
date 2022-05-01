using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll(bool listEntity);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);

        Task<T> GetById(int id);
        
        Task<T> GetById(int id, string include);
        
        Task<T> Add(T entity);
        Task<T> AddAsync(T entity);


        Task Update(T entity);
        
        Task Delete(int id);
    }
}
