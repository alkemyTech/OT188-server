using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Helper;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly OngProjectDbContext _context;
        protected DbSet<T> _entities;

        public Repository(OngProjectDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAll(bool listEntity)
        {
            if (!listEntity)
            {
                return await _entities.Where(x => x.IsDeleted == true).ToListAsync();
            }
            
            return await _entities.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
        {
            if (predicate == null)
            {
                return await _entities.IncludeMultiple(include).ToListAsync();
            }
            return await _entities.Where(predicate).IncludeMultiple(include).ToListAsync();

        }

        public async Task<T> GetById(int id)
        {
            var entity = await _entities.SingleOrDefaultAsync(x => x.Id == id);

            return entity;
        }       
        public async Task<T> GetById(int id, string include)
        {
            var entity = await _entities
                .Include(include)
                .SingleOrDefaultAsync(x => x.Id == id);

            return entity?.IsDeleted == false ? entity : null;
        }

        public async Task<T> Add(T entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null || entity.IsDeleted == true)
                throw new InvalidOperationException("Entity not found");

            entity.IsDeleted = true;
            _entities.Update(entity);
        }

        public async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> expression)
        {
            var collection = await _entities.Where(expression).ToListAsync();

            return collection;
        }

        public async Task<int> Count()
        {
            return await _entities.CountAsync();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, IList<Expression
                <Func<T, object>>> includes = null, int? page = null, int? pageSize = null)
        {
            var query = this._entities.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return await query.ToListAsync();
        }
    }
}