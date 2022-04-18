using OngProject.Core.Models;
using System;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Organizations> OrganizationsRepository { get; }

    {       
        IRepository<Roles> RolesRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();

        void Dispose();
    }
}
