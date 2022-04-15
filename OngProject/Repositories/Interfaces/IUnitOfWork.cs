using OngProject.Core.Models;
using System;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {       
        void SaveChanges();

        Task SaveChangesAsync();

        void Dispose();
    }
}
