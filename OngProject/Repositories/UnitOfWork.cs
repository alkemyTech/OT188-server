using OngProject.Repositories.Interfaces;
using OngProject.Core.Models;
using OngProject.DataAccess;
using System.Threading.Tasks;
using System;

namespace OngProject.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly OngProjectDbContext _context;

        private bool disposed = false;

        public UnitOfWork(OngProjectDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
      
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
