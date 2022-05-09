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

        Task Delete(int id);

        Task<T> Update(T entity);

        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> expression);

        Task<int> Count();

        Task<ICollection<T>> FindAllAsync(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             IList<Expression<Func<T, object>>> includes = null,
             int? page = null,
             int? pageSize = null);

    }
}
