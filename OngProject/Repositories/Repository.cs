using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<T>> GetAll(bool listEntity, string include)
        {
            if (!listEntity)
            {
                return await _entities.Where(x => x.IsDeleted == true).Include(include).ToListAsync();
            }
            
            return await _entities.Where(x => x.IsDeleted == false).Include(include).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var entity = await _entities.FindAsync(id);
            
            return entity.IsDeleted == false ? entity : null;
        }

        public async Task<T> GetById(int id, string include)
        {
            var entity = await _entities
                .Include(include)
                .SingleOrDefaultAsync(x => x.Id == id);

            return entity.IsDeleted == false ? entity : null;
        }

        public async Task<T> Add(T entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public async Task Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                _entities.Update(entity);
            }
        }
    }
}