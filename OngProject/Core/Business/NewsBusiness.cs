using System.Collections.Generic;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public NewsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<IEnumerable<New>> GetNews(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public Task<New> GetNew(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<New> InsertNew(New entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateNew(int id, New entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteNew(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}